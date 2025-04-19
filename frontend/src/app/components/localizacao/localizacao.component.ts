import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Localizacao } from '../../models/localizacao.model';
import { LocalizacaoService } from '../../services/localizacao.service';

@Component({
  selector: 'app-localizacao',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './localizacao.component.html',
  styleUrls: ['./localizacao.component.css'],
})
export class LocalizacaoComponent {
  bairroAtual: string = '';
  cidadeAtual: string = '';
  estadoAtual: string = '';
  categoriaSelecionada: string = '';
  localizacaoDetectada: boolean = false;
  erro: string | null = null;
  empresas: any[] = [];

  estados: string[] = [];
  cidades: string[] = [];
  bairros: string[] = [];

  estadoSelecionado = '';
  cidadeSelecionada = '';
  bairroSelecionado = '';

  mostrandoFormulario = false;

  constructor(private localizacaoService: LocalizacaoService) {}

  ngOnInit() {
    this.pegarLocalizacao();
  }

  pegarLocalizacao() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          const lat = position.coords.latitude;
          const lon = position.coords.longitude;

          this.localizacaoService.getLocation(lat, lon).subscribe({
            next: (res: Localizacao) => {
              this.bairroAtual = res.bairro ?? 'Bairro não identificado';
              this.cidadeAtual = res.cidade ?? 'Cidade não identificada';
              this.estadoAtual = res.estado ?? 'Estado não identificado';
              this.localizacaoDetectada = true;
              this.erro = null;

              this.localizacaoService.atualizarLocalizacao(
                this.estadoAtual,
                this.cidadeAtual,
                this.bairroAtual
              );
            },
            error: () => {
              this.erro = 'Erro ao buscar endereço.';
            },
          });
        },
        (error) => {
          this.erro = this.getGeolocationErrorMessage(error);
        }
      );
    } else {
      this.erro = 'Geolocalização não suportada.';
    }
  }

  private getGeolocationErrorMessage(error: GeolocationPositionError): string {
    switch (error.code) {
      case error.PERMISSION_DENIED:
        return 'Permissão de geolocalização negada.';
      case error.POSITION_UNAVAILABLE:
        return 'Localização indisponível.';
      case error.TIMEOUT:
        return 'Tempo esgotado.';
      default:
        return 'Erro desconhecido.';
    }
  }

  alterarLocalizacao() {
    if (this.mostrandoFormulario) {
      this.estadoAtual = this.estadoSelecionado;
      this.cidadeAtual = this.cidadeSelecionada;
      this.bairroAtual = this.bairroSelecionado;
      this.mostrandoFormulario = false;
      this.localizacaoDetectada = true;
      this.localizacaoService.atualizarLocalizacao(
        this.estadoAtual,
        this.cidadeAtual,
        this.bairroAtual
      );
    } else {
      this.localizacaoService.getEstados().subscribe({
        next: (res) => (this.estados = res),
        error: () => (this.erro = 'Erro ao carregar estados'),
      });
      this.mostrandoFormulario = true;
    }
  }

  onEstadoChange() {
    if (!this.estadoSelecionado) {
      this.cidadeSelecionada = '';
      this.bairroSelecionado = '';
      this.cidades = [];
      this.bairros = [];
      return;
    }

    this.localizacaoService.getCidades(this.estadoSelecionado).subscribe({
      next: (res) => {
        this.cidades = res;
        this.cidadeSelecionada = '';
        this.bairroSelecionado = '';
        this.bairros = [];
      },
      error: () => {
        this.erro = 'Erro ao carregar cidades';
      },
    });
  }

  onCidadeChange() {
    if (!this.cidadeSelecionada) {
      this.bairroSelecionado = '';
      this.bairros = [];
      return;
    }

    this.localizacaoService
      .getBairros(this.estadoSelecionado, this.cidadeSelecionada)
      .subscribe({
        next: (res) => {
          this.bairros = res;
          this.bairroSelecionado = '';
        },
        error: () => {
          this.erro = 'Erro ao carregar bairros';
        },
      });
  }
}
