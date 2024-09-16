import { Injectable } from '@angular/core';
import { WebRequestService } from '../web/web-request.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TasksService {
  constructor(private webService: WebRequestService) {}

  getTasks(): Observable<any[]> {
    return this.webService.getTasks('api/v1/tasks');
  }

  getTaskDetails(taskId: number): Observable<any> {
    return this.webService.getTaskById(`api/v1/tasks/${taskId}`);
  }

  create(newTask: any): Observable<any> {
    return this.webService.createTask('api/v1/tasks', newTask);
  }

  update(taskId: number, updatedTask: any): Observable<any> {
    return this.webService.updateTaskById(
      `api/v1/tasks/${taskId}`,
      updatedTask
    );
  }

  delete(taskId: number): Observable<any> {
    return this.webService.deleteTaskById(`api/v1/tasks/${taskId}`);
  }

  addComment(taskId: number, commentText: any): Observable<any> {
    return this.webService.AddTaskComment(
      `api/v1/tasks/${taskId}/comments`,
      commentText
    );
  }

  assignToTeam(taskId: number, team: any): Observable<any> {
    return this.webService.assignTaskToTeam(
      `api/v1/tasks/${taskId}/assign/team`,
      team
    );
  }

  assignToMember(taskId: number, member: any): Observable<any> {
    return this.webService.assignTaskToMember(
      `api/v1/tasks/${taskId}/assign/member`,
      member
    );
  }
}
