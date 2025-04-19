import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FiltroEmpresa } from '../models/filtro-empresa.model';

@Injectable({
  providedIn: 'root',
})
export class FiltroService {
  private filtroSubject = new BehaviorSubject<FiltroEmpresa>({});
  filtro$ = this.filtroSubject.asObservable();

  atualizarFiltro(filtro: FiltroEmpresa) {
    this.filtroSubject.next(filtro);
  }

  obterFiltroAtual(): FiltroEmpresa {
    return this.filtroSubject.value;
  }
}
