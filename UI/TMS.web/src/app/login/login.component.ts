import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { AuthintecationService } from '../services/authintecation/authintecation.service';
import { WebRequestService } from '../services/web/web-request.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private auth: AuthintecationService,
    private ser: WebRequestService
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: [''],
      password: [''],
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    const credentials = {
      email: this.f['email'].value,
      password: this.f['password'].value,
    };

    this.auth.login(credentials).subscribe((success) => {
      if (success) {
        console.log('Login successful');

        this.router.navigate(['dashboard']);
      } else {
        console.error('Login failed');
      }
    });
  }

  onReset() {
    this.submitted = false;
    this.loginForm.reset();

    this.router.navigate(['/']);
  }
}
