import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TasksService } from '../../services/tasks/tasks.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrl: './create-task.component.css',
})
export class CreateTaskComponent {
  newTask = {
    title: '',
    description: '',
    status: 0,
    priorityLevel: 0,
    dueDate: '',
  };

  constructor(private router: Router, private taskService: TasksService) {}

  onSubmit() {
    if (this.newTask.title && this.newTask.dueDate) {
      this.taskService.create(this.newTask).subscribe({
        next: (reponse) => {
          console.log('task created successfuly: ', reponse);
          this.router.navigate(['dashboard']);
        },
        error: (err) => {
          console.log('task creation faild: ', err);
        },
      });
    }
  }

  onCancel() {
    console.log('Task creation canceled.');
    this.router.navigate(['dashboard']);
  }

  resetForm() {
    this.newTask = {
      title: '',
      description: '',
      status: 0,
      priorityLevel: 0,
      dueDate: '',
    };
  }
}
