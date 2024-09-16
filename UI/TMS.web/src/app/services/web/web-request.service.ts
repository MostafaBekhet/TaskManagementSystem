import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpHeaderService } from '../httpHeader/http-header.service';

@Injectable({
  providedIn: 'root',
})
export class WebRequestService {
  readonly ROOT_URL;

  private accessToken: string | null = null;
  private refreshToken: string | null = null;

  constructor(
    private http: HttpClient,
    private httpHeaders: HttpHeaderService
  ) {
    this.ROOT_URL = 'http://localhost:5091';
  }

  registerUser(uri: string, user: any): Observable<any> {
    console.log(user);
    const headers = this.httpHeaders.getHeaders();
    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, user, { headers });
  }

  loginUser(uri: string, credentials: any): Observable<boolean> {
    const headers = this.httpHeaders.getHeaders();
    return this.http
      .post<any>(`${this.ROOT_URL}/${uri}`, credentials, {
        headers,
      })
      .pipe(
        map((response) => {
          // Store the access token
          this.accessToken = response.accessToken;
          localStorage.setItem('accessToken', this.accessToken!);
          this.refreshToken = response.refreshToken;
          return true;
        }),
        catchError(() => {
          // Handle errors here
          return of(false);
        })
      );
  }

  setAcessToken(accessToken: string) {
    this.accessToken = accessToken;
  }

  getAccessToken(): string | null {
    const token = localStorage.getItem('accessToken');
    return (this.accessToken = token);
  }

  getAccessRefreshToken(): string | null {
    return this.refreshToken;
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    this.accessToken = null;
    this.refreshToken = null;
  }

  createTask(uri: string, newTask: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );
    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, newTask, { headers });
  }

  getTasks(uri: string): Observable<any[]> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );
    return this.http.get<any[]>(`${this.ROOT_URL}/${uri}`, { headers });
  }

  getTaskById(uri: string): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.get<any>(`${this.ROOT_URL}/${uri}`, { headers });
  }

  updateTaskById(uri: string, updatedTask: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.put<any>(`${this.ROOT_URL}/${uri}`, updatedTask, {
      headers,
    });
  }

  deleteTaskById(uri: string): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.delete<any>(`${this.ROOT_URL}/${uri}`, { headers });
  }

  AddTaskComment(uri: string, commentText: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    const commnetjson = `"${commentText}"`;

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, commnetjson, {
      headers,
    });
  }

  getTeams(uri: string): Observable<any[]> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.get<any[]>(`${this.ROOT_URL}/${uri}`, { headers });
  }

  createTeam(uri: string, newTeam: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, newTeam, {
      headers,
    });
  }

  assignTaskToTeam(uri: string, team: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, team, { headers });
  }

  assignTaskToMember(uri: string, member: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, member, { headers });
  }

  deleteTeam(uri: string): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    return this.http.delete<any>(`${this.ROOT_URL}/${uri}`, { headers });
  }

  addTeamMember(uri: string, email: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    const emailJson = `"${email}"`;

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, emailJson, {
      headers,
    });
  }

  removeTeamMember(uri: string, email: any): Observable<any> {
    const headers = this.httpHeaders.getHeaders(
      this.getAccessToken() ?? undefined
    );

    const emailJson = `"${email}"`;

    return this.http.post<any>(`${this.ROOT_URL}/${uri}`, emailJson, {
      headers,
    });
  }
}
