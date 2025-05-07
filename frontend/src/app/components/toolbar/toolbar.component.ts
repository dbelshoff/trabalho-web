import { Component, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { ClienteService } from '../../services/cliente.service';
import { Cliente } from '../../models/cliente.model';
import { Observable } from 'rxjs';
import { ViewportScroller } from '@angular/common';

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
  cliente: Cliente | null = null;
  cliente$!: Observable<Cliente | null>;

  constructor(
    private router: Router,
    private clienteService: ClienteService,
    public authService: AuthService,
    private viewportScroller: ViewportScroller
  ) {}

  ngOnInit(): void {
    this.authService.cliente$.subscribe((cliente) => {
      this.cliente = cliente;
    });
  }

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
