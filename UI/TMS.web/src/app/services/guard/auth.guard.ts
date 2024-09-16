import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthintecationService } from '../authintecation/authintecation.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthintecationService,
    private router: Router
  ) {}

  canActivate(): boolean {
    // Check if the user is authenticated
    if (this.authService.loggedIn()) {
      return true; // Allow access to the route
    } else {
      // Redirect to the login page
      this.router.navigate(['/login']);
      return false; // Block access to the route
    }
  }
}
