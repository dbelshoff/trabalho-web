import { Endereco } from './endereco.models';

export interface Empresa {
  id: number;
  nome?: string;
  telefone?: string;
  instagram?: string;
  site?: string;
  requerido: boolean;
  nivel: string;
  endereco?: Endereco[];
}
