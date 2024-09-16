import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateTaskComponent } from './subComponents/create-task/create-task.component';
import { TaskDetailsComponent } from './subComponents/task-details/task-details.component';
import { TeamsComponent } from './teams/teams.component';

import { AuthGuard } from './services/guard/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'tasks/create',
    component: CreateTaskComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'tasks/:taskId',
    component: TaskDetailsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'teams',
    component: TeamsComponent,
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
