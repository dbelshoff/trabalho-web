import { Component, Input, OnInit } from '@angular/core';
import { Empresa } from '../../models/empresa.model';
import { CommonModule } from '@angular/common';
import { EnderecoService } from '../../services/endereco.service';
import { Endereco } from '../../models/endereco.models';
//import { EncodeURIComponentPipe } from '../../pipes/encode-uri-component.pipe.ts.service';

@Component({
  selector: 'app-empresa-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './empresa-card.component.html',
  styleUrls: ['./empresa-card.component.css'],
})
export class EmpresaCardComponent implements OnInit {
  @Input() empresa!: Empresa; // A propriedade que receberá os dados da empresa
  enderecos: Endereco[] = [];
  mostrarContato: boolean = false;
  //empresa: any;
  //enderecos: any[];

  constructor(private enderecoService: EnderecoService) {}

  ngOnInit(): void {
    this.buscarEndereço();
  }

  buscarEndereço() {
    this.enderecoService
      .getEnderecoPorEmpresa(this.empresa.id)
      .subscribe((res: Endereco[]) => {
        this.enderecos = res;
      });
  }

  getMapsUrl(endereco: any): string {
    const query = `${endereco.logradouro}, ${endereco.numero}, ${endereco.bairro}, ${endereco.cidade}, ${endereco.estado}`;
    return `https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(
      query
    )}`;
  }

  openMaps(endereco: any): void {
    window.open(this.getMapsUrl(endereco), '_blank', 'noopener,noreferrer');
  }

  toggleContato(): void {
    this.mostrarContato = !this.mostrarContato;
  }
}
