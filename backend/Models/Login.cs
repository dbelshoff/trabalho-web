using System.ComponentModel.DataAnnotations;

namespace projetoX.Models;

public class Login
{
    public string Email { get; set; }
    public string Senha { get; set; }
}