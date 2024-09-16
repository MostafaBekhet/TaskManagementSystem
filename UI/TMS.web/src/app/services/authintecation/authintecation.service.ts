import { Injectable } from '@angular/core';
import { WebRequestService } from '../web/web-request.service';

@Injectable({
  providedIn: 'root',
})
export class AuthintecationService {
  constructor(private webRequestService: WebRequestService) {}

  register(user: any) {
    return this.webRequestService.registerUser(
      'api/v1/identity/registerCustom',
      user
    );
  }

  login(user: any) {
    return this.webRequestService.loginUser('api/v1/identity/login', user);
  }

  loggedIn(): boolean {
    const token = localStorage.getItem('accessToken');

    if (token) {
      this.webRequestService.setAcessToken(token);
    }

    return token !== null;
  }

  setToke(accessToken: string) {
    this.webRequestService.setAcessToken(accessToken);
  }

  logUserOut() {
    this.webRequestService.logout();
  }
}
