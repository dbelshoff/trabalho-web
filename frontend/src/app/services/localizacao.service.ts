import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Localizacao } from '../models/localizacao.model';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class LocalizacaoService {
  private apiUrl = `${environment.apiUrl}/enderecos`;

  estado: string = '';
  cidade: string = '';
  bairro: string = '';

  constructor(private http: HttpClient) {}

  private localizacaoSubject = new BehaviorSubject<boolean>(false);
  localizacaoAtualizada$ = this.localizacaoSubject.asObservable();

  atualizarLocalizacao(estado: string, cidade: string, bairro: string) {
    this.estado = estado;
    this.cidade = cidade;
    this.bairro = bairro;
    this.localizacaoSubject.next(true); // emite evento de atualização
  }

  getLocation(latitude: number, longitude: number): Observable<Localizacao> {
    return this.http.get<Localizacao>(
      `${this.apiUrl}/geolocalizacao?lat=${latitude}&lon=${longitude}`
    );
  }

  getEstados(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/estados`);
  }

  getCidades(estado: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/cidades`, {
      params: { estado },
    });
  }

  getBairros(estado: string, cidade: string): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/bairros`, {
      params: { estado, cidade },
    });
  }
}
