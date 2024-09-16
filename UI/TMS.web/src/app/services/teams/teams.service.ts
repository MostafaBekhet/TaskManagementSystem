import { Injectable } from '@angular/core';
import { WebRequestService } from '../web/web-request.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TeamsService {
  constructor(private webService: WebRequestService) {}

  getTeams(): Observable<any[]> {
    return this.webService.getTeams('api/v1/teams');
  }

  createTeam(newTeam: any): Observable<any> {
    return this.webService.createTeam(`api/v1/teams`, newTeam);
  }

  deleteTeam(teamId: number): Observable<any> {
    return this.webService.deleteTeam(`api/v1/teams/${teamId}`);
  }

  addMember(teamId: number, memberEmail: any): Observable<any> {
    return this.webService.addTeamMember(
      `api/v1/teams/${teamId}/adduser`,
      memberEmail
    );
  }

  removeMember(teamId: number, memberEmail: any): Observable<any> {
    return this.webService.removeTeamMember(
      `api/v1/teams/${teamId}/removeuser`,
      memberEmail
    );
  }
}
