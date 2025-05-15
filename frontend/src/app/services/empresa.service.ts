import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Empresa } from '../models/empresa.model'; // Ajuste o caminho conforme necessário
import { FiltroEmpresa } from '../models/filtro-empresa.model';
import { environment } from '../../environment/environment';

@Injectable({
  providedIn: 'root',
})
export class EmpresaService {
  //private apiUrlFiltro = '/api/empresas/por-endereco';
  //private apiUrlAll = '/api/empresas';
  //private apiUrlFiltroCompleto =
  //'${environment.apiUrl}/empresas/filtrar-completo';
  private apiUrl = `${environment.apiUrl}/empresas/filtrar-completo`;

  constructor(private http: HttpClient) {}

  /*getAllEmpresas(): Observable<Empresa[]> {
    return this.http.get<Empresa[]>(this.apiUrlAll);
  }

  getEmpresasFiltradas(filtro: FiltroEmpresa): Observable<Empresa[]> {
    let params = new HttpParams();

    if (filtro.estado) params = params.set('estado', filtro.estado);
    if (filtro.cidade) params = params.set('cidade', filtro.cidade);
    if (filtro.bairro) params = params.set('bairro', filtro.bairro);

    return this.http.get<Empresa[]>(this.apiUrlFiltro, { params });
  }*/

  getEmpresasFiltradas(filtro: FiltroEmpresa): Observable<Empresa[]> {
    let params = new HttpParams();

    if (filtro.categoria) params = params.set('categoria', filtro.categoria);
    if (filtro.estado) params = params.set('estado', filtro.estado);
    if (filtro.cidade) params = params.set('cidade', filtro.cidade);
    if (filtro.bairro) params = params.set('bairro', filtro.bairro);

    return this.http.get<Empresa[]>(this.apiUrl, { params });
  }
}
