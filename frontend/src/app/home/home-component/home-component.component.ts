import { LocalizacaoComponent } from './../../components/localizacao/localizacao.component';
import { Component } from '@angular/core';
import { EmpresaListaComponent } from '../../components/empresa/empresa-lista.component';

@Component({
  selector: 'app-home-component',
  imports: [LocalizacaoComponent, EmpresaListaComponent],
  templateUrl: './home-component.component.html',
  styleUrl: './home-component.component.css',
})
export class HomeComponent {}
