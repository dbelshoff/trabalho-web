import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'toolbar',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    RouterModule,
    CommonModule,
  ],
  templateUrl: 'toolbar.component.html',
  styleUrl: 'toolbar.component.css',
})
export class ToolbarComponent {
  constructor(private router: Router, public authService: AuthService) {}

  irParaCadastro() {
    this.router.navigate(['/cadastro-cliente']);
  }

  irParaLogin() {
    this.router.navigate(['/login']);
  }

  toHome() {
    this.router.navigate(['/home']);
  }

  sair() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
