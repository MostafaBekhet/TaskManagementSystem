import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TasksService } from '../../services/tasks/tasks.service';
import moment from 'moment';
import { animate } from '@angular/animations';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrl: './task-details.component.css',
})
export class TaskDetailsComponent implements OnInit {
  private taskId: number = 0;

  task: any = {};
  comments: any[] = [];
  assignedTeam: any = {};
  assignedUser: any = {};

  originalTask: any = {};
  updateTask: any = {};

  commentText: string = '';

  teamId: number = 0;

  memberEmail: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TasksService
  ) {}

  ngOnInit(): void {
    this.taskId = parseInt(this.route.snapshot.paramMap.get('taskId')!, 10);

    this.taskService.getTaskDetails(this.taskId).subscribe({
      next: (response) => {
        this.task = response;

        this.comments = this.task.comments;

        this.assignedTeam = this.task.assignedTeam;

        this.assignedUser = this.task.assignedUser;

        if (this.task.dueDate) {
          this.task.dueDate = this.formatDateForInput(this.task.dueDate);
        }

        this.fillTask();

        console.log('task fetched successfuly.');
      },
      error: (err) => {
        console.error('task fetching faild', err);
      },
    });
  }

  fillTask() {
    this.originalTask = {
      title: this.task.title,
      description: this.task.description,
      status: this.task.status,
      priorityLevel: this.task.priorityLevel,
      dueDate: this.task.dueDate,
    };
  }

  hasChanges(): boolean {
    return (
      this.task.title !== this.originalTask.title ||
      this.task.description !== this.originalTask.description ||
      parseInt(this.task.status) !== this.originalTask.status ||
      parseInt(this.task.priorityLevel) !== this.originalTask.priorityLevel ||
      this.task.dueDate !== this.originalTask.dueDate
    );
  }

  onSave(): void {
    this.updateTask = {
      title: this.task.title,
      description: this.task.description,
      status: this.task.status,
      priorityLevel: this.task.priorityLevel,
      dueDate: this.task.dueDate,
    };

    this.taskService.update(this.taskId, this.updateTask).subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigate(['dashboard']);
      },

      error: (err) => {
        console.error(err);
      },
    });
  }

  onClose(): void {
    this.router.navigate(['/dashboard']);
  }

  getTimeAgo(dateTime: string): string {
    const date = moment(dateTime);

    return date.fromNow();
  }

  formatDateForInput(dateString: string): string {
    return new Date(dateString).toISOString().split('T')[0];
  }

  openCommentModel() {
    const modelDiv = document.getElementById('commentModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  closeCommentMode() {
    const modelDiv = document.getElementById('commentModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }

  addCommnet() {
    console.log('text: ', this.commentText);
    this.taskService.addComment(this.taskId, this.commentText).subscribe({
      next: (response) => {
        console.log(response);
        window.location.reload();
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  openAssignTeamModel() {
    const modelDiv = document.getElementById('assignTeamModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  assignTeam() {
    this.taskService
      .assignToTeam(this.taskId, { teamId: this.teamId })
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.reload();
        },
        error: (err) => {
          if (err.status == 403) {
            alert('Forbidden you can not assign task to team');
          } else if (err.status == 404) {
            alert('Team not found');
          } else {
            console.error(err);
          }
        },
      });
  }

  closeAssignTeamModel() {
    const modelDiv = document.getElementById('assignTeamModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }

  openAssignMemberModel() {
    const modelDiv = document.getElementById('assignMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  assignMember() {
    this.taskService
      .assignToMember(this.taskId, { userEmail: this.memberEmail })
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.reload();
        },
        error: (err) => {
          if (err.status == 403) {
            alert('Forbidden you can not assign task to member');
          } else if (err.status == 404) {
            alert('Member not found');
          } else {
            console.error(err);
          }
        },
      });
  }

  closeAssignMemberModel() {
    const modelDiv = document.getElementById('assignMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }
}
