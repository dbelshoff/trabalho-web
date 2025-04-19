import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CategoriaService } from '../../services/categoria.service';
import { Categoria } from '../../models/categoria.model';

@Component({
  selector: 'app-categoria',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './categoria.component.html',
  styleUrls: ['./categoria.component.css'],
})
export class CategoriaComponent implements OnInit {
  categoriaPesquisada: string = '';
  todasCategorias: Categoria[] = [];
  categoriasFiltradas: Categoria[] = [];
  @Output() categoriaSelecionada = new EventEmitter<string>();

  constructor(private categoriaService: CategoriaService) {}

  ngOnInit(): void {
    this.categoriaService.getCategoria().subscribe({
      next: (res) => {
        this.todasCategorias = res;
      },
    });
  }

  filtrarCategorias() {
    const termo = this.categoriaPesquisada.toLowerCase();
    this.categoriasFiltradas = this.todasCategorias.filter((cat) =>
      cat.nome?.toLowerCase().includes(termo)
    );
  }

  selecionarCategoria(categoria: string) {
    this.categoriaPesquisada = categoria;
    this.categoriasFiltradas = [];
    this.categoriaSelecionada.emit(categoria);
  }

  limparInput() {
    this.categoriaPesquisada = '';
    this.categoriasFiltradas = [];
  }
}
