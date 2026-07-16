import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  username = '';
  password = '';
  loginFailed = false;

  constructor(private readonly authService: AuthService, private readonly router: Router) {}

  onSubmit(form: NgForm): void {
    if (form.invalid) {
      return;
    }

    const success = this.authService.login({ username: this.username, password: this.password });
    this.loginFailed = !success;

    if (success) {
      this.router.navigate(['/books']);
    }
  }
}
