import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Cliente } from '../models/cliente.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private clienteLogado = new BehaviorSubject<Cliente | null>(null);
  private apiUrl = 'api/cliente/login';

  constructor(private http: HttpClient, private router: Router) {
    const clienteSalvo = localStorage.getItem('clienteLogado');
    if (clienteSalvo) {
      this.clienteLogado.next(JSON.parse(clienteSalvo));
    }
  }

  loginCliente(loginData: { email: string; senha: string }): Observable<any> {
    return this.http.post(this.apiUrl, loginData).pipe(
      tap((response: any) => {
        console.log('Resposta recebida:', response);
        const cliente = {
          nome: this.decodeJwt(response.token).name,
          email: loginData.email,
          telefone: '',
          senhaHash: '',
        };
        this.clienteLogado.next(cliente);
        localStorage.setItem('clienteLogado', JSON.stringify(cliente));
        localStorage.setItem('accessToken', response.Token);
        localStorage.setItem('refreshToken', response.RefreshToken);
      })
    );
  }

  logout(): void {
    this.clienteLogado.next(null);
    localStorage.removeItem('clienteLogado');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    this.router.navigate(['/home']);
  }

  isLoggedIn(): boolean {
    return !!this.clienteLogado.value && !!localStorage.getItem('accessToken');
  }

  getClienteLogado(): Observable<Cliente | null> {
    return this.clienteLogado.asObservable();
  }

  getNomeCliente(): string {
    return this.clienteLogado.value?.nome || '';
  }

  private decodeJwt(token: string): any {
    try {
      const payload = token.split('.')[1];
      // Corrige Base64Url para Base64 tradicional
      const base64 = payload.replace(/-/g, '+').replace(/_/g, '/');
      // Preenche com "=" até o comprimento ser múltiplo de 4
      const padded = base64.padEnd(
        base64.length + ((4 - (base64.length % 4)) % 4),
        '='
      );
      return JSON.parse(atob(padded));
    } catch (e) {
      console.error('Erro ao decodificar JWT:', e);
      return {};
    }
  }

  refreshToken(): Observable<any> {
    const refreshToken = localStorage.getItem('refreshToken');
    return this.http.post(`${this.apiUrl}/refresh`, { refreshToken }).pipe(
      tap((response: any) => {
        localStorage.setItem('accessToken', response.Token);
        localStorage.setItem('refreshToken', response.RefreshToken);
      })
    );
  }
}
