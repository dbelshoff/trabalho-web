import { Endereco } from './../models/endereco.models';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { Empresa } from '../models/empresa.model'; // Ajuste o caminho conforme necessário

@Injectable({
  providedIn: 'root',
})
export class EnderecoService {
  private apiUrl = 'api/enderecos/por-empresa';

  constructor(private http: HttpClient) {}

  getEnderecoPorEmpresa(empresaId: number): Observable<Endereco[]> {
    return this.http.get<Endereco[]>(`api/enderecos/por-empresa/${empresaId}`);
  }
}
