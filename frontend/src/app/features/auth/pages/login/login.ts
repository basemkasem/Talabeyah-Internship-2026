import { Component, ElementRef, inject, signal, viewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services';
import { form, FormField, FormRoot, minLength, required } from '@angular/forms/signals';

@Component({
  selector: 'app-login',
  imports: [FormField, FormRoot],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private authService = inject(AuthService);
  private router = inject(Router);
  isLoading: boolean = false;
  isLoadingSignal = signal<boolean>(false);

  loginModel = signal<LoginRequest>({
    username: '',
    password: '',
  });

  //summaryAlert = document.getElementById('summary-alert') as HTMLElement;

  alert = viewChild<ElementRef<HTMLDivElement>>('alert');

  loginForm = form(
    this.loginModel,
    (params) => {
      (required(params.username, { message: 'username is required' }),
        minLength(params.username, 3, { message: 'username should be at least 3 characters' }),
        required(params.password, { message: 'password is required' }));
    },
    {
      submission: {
        action: async () => {
          this.submitForm();
        }
      }
    }
  );

  submitForm() {
    let requestParams: LoginRequest = {
      username: this.loginModel().username,
      password: this.loginModel().password,
    };
    this.authService.login(requestParams).subscribe({
      next: (value) => {
        this.router.navigate(['/products']);
      },
      error: (err) => {
        console.error(err);
      },
    });
  }
}
