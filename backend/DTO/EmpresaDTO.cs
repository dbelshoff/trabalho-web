namespace projetoX.DTOs;

public class EmpresaDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string Telefone { get; set; }
    public string Instagram { get; set; }
    public string Site { get; set; }
    public bool Requerido {get; set;}
    public string Nivel {get; set;}

    public List<CategoriaDTO> Categorias { get; set; }

    public List<EnderecoDTO> Enderecos { get; set; } 

}
