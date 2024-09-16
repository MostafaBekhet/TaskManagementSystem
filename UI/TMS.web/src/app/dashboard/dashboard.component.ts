import { Component, OnInit } from '@angular/core';
import { TasksService } from '../services/tasks/tasks.service';
import { Router } from '@angular/router';
import { errorContext } from 'rxjs/internal/util/errorContext';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  tasks: any[] = [];

  constructor(private taskService: TasksService, private router: Router) {}

  ngOnInit(): void {
    this.taskService.getTasks().subscribe({
      next: (data) => {
        this.tasks = data;
      },
      error: (err) => {
        console.error('Faild fetch user tasks', err);
      },
      complete: () => {
        console.log('User Tasks fetched Successfully.');
      },
    });
  }

  createNewTask() {
    this.router.navigate(['tasks/create']);
  }

  getStatus(status: number): string {
    switch (status) {
      case 0:
        return 'To Do';
      case 1:
        return 'In Progress';
      case 2:
        return 'Completed';
      default:
        return 'unknown';
    }
  }

  getPriority(priorityLevel: number): string {
    switch (priorityLevel) {
      case 0:
        return 'Low';
      case 1:
        return 'Medium';
      case 2:
        return 'High';
      default:
        return 'unknown';
    }
  }

  viewDetails(taskId: number): void {
    this.router.navigate([`tasks/${taskId}`]);
  }

  deleteTask(taskId: number): void {
    this.taskService.delete(taskId).subscribe({
      next: (response) => {
        console.log(response);
        window.location.reload();
      },

      error: (err) => {
        console.error(err);
      },
    });
  }
}
