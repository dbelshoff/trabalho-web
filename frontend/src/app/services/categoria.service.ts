import { Categoria } from './../models/categoria.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  constructor(private http: HttpClient) {}

  getCategoria(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(`/api/categorias`);
  }
}
