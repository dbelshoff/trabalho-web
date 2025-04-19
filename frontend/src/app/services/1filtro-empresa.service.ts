import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FiltroEmpresa } from '../models/filtro-empresa.model';

@Injectable({
  providedIn: 'root',
})
export class FiltroEmpresaService {
  private filtroSubject = new BehaviorSubject<FiltroEmpresa>({
    estado: '',
    cidade: '',
    bairro: '',
  });

  filtro$ = this.filtroSubject.asObservable();

  atualizarFiltro(filtro: FiltroEmpresa) {
    this.filtroSubject.next(filtro);
  }
}
