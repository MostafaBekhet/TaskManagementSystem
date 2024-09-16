import { Component, OnInit } from '@angular/core';
import { TeamsService } from '../services/teams/teams.service';

@Component({
  selector: 'app-teams',
  templateUrl: './teams.component.html',
  styleUrl: './teams.component.css',
})
export class TeamsComponent implements OnInit {
  teams: any[] = [];

  teamId: number = 0;

  expandedTeamId: number | null = null;
  expandedTasksTeamId: number | null = null;

  newTeam: any = {};
  newTeamName: string = '';

  addMemberEmail: string = '';
  removeMemberEmail: string = '';

  constructor(private teamService: TeamsService) {}

  ngOnInit(): void {
    this.teamService.getTeams().subscribe({
      next: (res) => {
        this.teams = res;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  openCreateTeamModel() {
    this.newTeamName = '';

    const modelDiv = document.getElementById('createTeamModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  closeCreateTeamModel() {
    const modelDiv = document.getElementById('createTeamModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }

  createTeam() {
    this.newTeam = {
      teamName: this.newTeamName,
    };

    this.teamService.createTeam(this.newTeam).subscribe({
      next: (res) => {
        console.log(res);
        window.location.reload();
      },
      error: (err) => {
        if (err.status == 403) {
          alert('Forbidden you can not create team!');
        } else {
          console.error(err);
        }
      },
    });
  }

  deleteTeam(teamId: number) {
    this.teamService.deleteTeam(teamId).subscribe({
      next: (res) => {
        console.log(res);
        window.location.reload();
      },
      error: (err) => {
        if (err.status == 403) {
          alert('Forbidden you can not delete team!');
        } else {
          console.error(err);
        }
      },
    });
  }

  toggleMembers(teamId: number) {
    if (this.expandedTeamId === teamId) {
      this.expandedTeamId = null;
    } else {
      this.expandedTeamId = teamId;
    }
  }

  toggleAssignedTasks(teamId: number) {
    if (this.expandedTasksTeamId === teamId) {
      this.expandedTasksTeamId = null;
    } else {
      this.expandedTasksTeamId = teamId;
    }
  }

  openaddMemberModel(teamId: number) {
    this.teamId = teamId;

    this.addMemberEmail = '';

    const modelDiv = document.getElementById('addMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  closeAddMemberModel() {
    const modelDiv = document.getElementById('addMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }

  addMember() {
    this.teamService.addMember(this.teamId, this.addMemberEmail).subscribe({
      next: (res) => {
        console.log(res);
        window.location.reload();
      },
      error: (err) => {
        if (err.status == 403) {
          alert('Forbidden you can not add member to team');
        } else {
          console.error(err);
        }
      },
    });
  }

  openRemoveMemberModel(teamId: number) {
    this.teamId = teamId;

    this.removeMemberEmail = '';

    const modelDiv = document.getElementById('removeMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'block';
    }
  }

  closeRemoveMemberModel() {
    const modelDiv = document.getElementById('removeMemberModel');

    if (modelDiv != null) {
      modelDiv.style.display = 'none';
    }
  }

  removeMember() {
    this.teamService
      .removeMember(this.teamId, this.removeMemberEmail)
      .subscribe({
        next: (res) => {
          console.log(res);
          window.location.reload();
        },
        error: (err) => {
          if (err.status == 403) {
            alert('Forbidden you can not remove member from team');
          } else {
            console.error(err);
          }
        },
      });
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

  formatDateForInput(dateString: string): string {
    return new Date(dateString).toISOString().split('T')[0];
  }
}
