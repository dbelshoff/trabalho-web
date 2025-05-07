/*import { FormsModule } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { EmpresaService } from '../../services/empresa.service';
import { Empresa } from '../../models/empresa.model';
import { EmpresaCardComponent } from './empresa-card.component';
import { CommonModule } from '@angular/common';
import { EnderecoService } from '../../services/endereco.service';
import { LocalizacaoService } from '../../services/localizacao.service';

import { LocalizacaoComponent } from '../localizacao/localizacao.component';
import { CategoriaComponent } from '../categoria/categoria.component';
import { FiltroService } from '../../services/filtro.services';
import { FiltroEmpresa } from '../../models/filtro-empresa.model';

@Component({
  selector: 'app-empresa-lista',
  standalone: true,
  imports: [EmpresaCardComponent, CommonModule, FormsModule],
  templateUrl: './empresa-lista.component.html',
  styleUrls: ['./empresa-lista.component.css'],
})
export class EmpresaListaComponent implements OnInit {
  empresas: Empresa[] = [];
  estadoSelecionado?: string;
  cidadeSelecionada?: string;
  bairroSelecionado?: string;

  constructor(
    private empresaService: EmpresaService,
    private enderecoService: EnderecoService
  ) {}

  ngOnInit() {}

  filtrarEmpresas() {
    const filtro: FiltroEmpresa = {
      estado: this.estadoSelecionado || undefined,
      cidade: this.cidadeSelecionada || undefined,
      bairro: this.bairroSelecionado || undefined,
    };
    console.log('🔎 Filtro final enviado para API:', filtro);

    this.empresaService.getEmpresasFiltradas(filtro).subscribe({
      next: (res) => {
        this.empresas = res;
        console.log('Empresas retornadas:', res);
      },
      error: () => {
        'Erro ao filtrar empresas.';
      },
    });
  }
}*/
import { CommonModule } from '@angular/common';
import { EmpresaCardComponent } from './empresa-card.component';
import { Component, OnInit } from '@angular/core';
import { EmpresaService } from '../../services/empresa.service'; // Ajuste o caminho
import { Empresa } from '../../models/empresa.model';
//import { FiltroService } from '../../services/filtro.services';
import { LocalizacaoService } from '../../services/localizacao.service';
import { FiltroEmpresa } from '../../models/filtro-empresa.model';
import { CategoriaComponent } from '../categoria/categoria.component';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-empresa-lista',
  standalone: true,
  imports: [EmpresaCardComponent, CommonModule, CategoriaComponent],
  templateUrl: './empresa-lista.component.html',
  styleUrls: ['./empresa-lista.component.css'],
})
export class EmpresaListaComponent implements OnInit {
  empresas: Empresa[] = [];
  categoriaSelecionada: string = '';
  bairro: string = '';
  cidade: string = '';
  estado: string = '';
  selectLocalizacao = true;
  nenhumaEmpresaEncontrada: boolean = true;

  constructor(
    private empresaService: EmpresaService,
    private localizacaoService: LocalizacaoService
  ) {}

  @ViewChild(CategoriaComponent) categoriaComponent!: CategoriaComponent;

  ngOnInit() {
    this.localizacaoService.localizacaoAtualizada$.subscribe(() => {
      this.carregarempresas();
    });
  }

  carregarEmpresasPorCategoria(categoria: string) {
    this.categoriaSelecionada = categoria;
    this.carregarempresas();
  }

  carregarempresas() {
    this.bairro = this.localizacaoService.bairro;
    this.cidade = this.localizacaoService.cidade;
    this.estado = this.localizacaoService.estado;

    const filtro: FiltroEmpresa = {
      categoria: this.categoriaSelecionada || undefined,
      bairro: this.bairro || undefined,
      cidade: this.cidade || undefined,
      estado: this.estado || undefined,
    };
    if (this.estado == '') {
      this.selectLocalizacao = false;
    } else {
      this.selectLocalizacao = true;
      this.empresaService.getEmpresasFiltradas(filtro).subscribe({
        next: (empresas: Empresa[]) => {
          this.empresas = empresas;
          this.nenhumaEmpresaEncontrada = empresas.length === 0;
        },
        error: () => {
          console.error('Erro ao carregar empresas');
        },
      });
    }
  }

  limparCategoria() {
    this.categoriaSelecionada = '';
    this.carregarempresas();
    this.categoriaComponent.limparInput();
  }
}
