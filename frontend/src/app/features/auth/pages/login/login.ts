import { Component, inject } from '@angular/core';
import { AuthService } from '../../services';

@Component({
  selector: 'app-login',
  imports: [],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  private authService = inject(AuthService);
}
