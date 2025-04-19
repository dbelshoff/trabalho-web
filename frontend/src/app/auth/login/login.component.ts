import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginData = { email: '', senha: '' };
  mensagem: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login(form: any) {
    if (form.valid) {
      console.log(this.loginData);
      this.authService.loginCliente(this.loginData).subscribe({
        next: (response) => {
          this.mensagem = 'Login realizado com sucesso!';
          setTimeout(() => {
            this.mensagem = '';
            this.loginData = { email: '', senha: '' };

            form.resetForm();
            this.router.navigate(['/home']);
          }, 2000);
        },
        error: (err) => {
          this.mensagem = 'Email ou senha inválidos.';
          console.error('Erro no login:', err);
        },
      });
    }
  }
}
