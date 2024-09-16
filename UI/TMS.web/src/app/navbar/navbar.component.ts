import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthintecationService } from '../services/authintecation/authintecation.service';

@Component({
  selector: 'app-navbar',
  //standalone: true,
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
  //imports: [RouterLink],
})
export class NavbarComponent {
  constructor(private router: Router, private auth: AuthintecationService) {}

  navigateToRegister() {
    this.router.navigate(['/register']);
  }

  isLoggedIn(): boolean {
    return this.auth.loggedIn();
  }

  onLogout(): void {
    this.auth.logUserOut();

    this.router.navigate(['/']);
  }
}
