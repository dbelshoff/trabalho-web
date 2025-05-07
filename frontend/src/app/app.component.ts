import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
//import { CategoriaComponent } from './components/categoria/categoria.component';
import { EmpresaListaComponent } from './components/empresa/empresa-lista.component';
import { LocalizacaoComponent } from './components/localizacao/localizacao.component';
import { FooterComponent } from './components/footer/footer.component';
//import { CadastroClienteComponent } from './components/cadastro-cliente/cadastro-cliente.component';
//import { routes } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToolbarComponent, CommonModule, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'frontend';
}
