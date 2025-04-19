using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using projetoX.Data;
using projetoX.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace projetoX.Controllers;

[Route("api/cliente/login")]
[AllowAnonymous]
[ApiController]
public class ClienteLoginController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public ClienteLoginController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult Login([FromBody] ClienteLogin login)
    {
        var cliente = _context.Clientes.FirstOrDefault(c => c.Email == login.Email);

        if (cliente == null || !BCrypt.Net.BCrypt.Verify(login.Senha, cliente.SenhaHash))
            return Unauthorized("Email ou senha inválidos.");

        var accessToken = GenerateJwtToken(cliente);
        var refreshToken = GenerateRefreshToken(cliente.Email);

        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();

        return Ok(new
        {
            Token = accessToken,
            RefreshToken = refreshToken.Token
        });
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        var tokenExistente = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken && rt.Ativo);

        if (tokenExistente == null || tokenExistente.Expiration < DateTime.UtcNow)
            return Unauthorized("Refresh token inválido ou expirado.");

        var cliente = _context.Clientes.FirstOrDefault(c => c.Email == tokenExistente.EmailUsuario);
        if (cliente == null)
            return Unauthorized("Cliente não encontrado.");

        var novoAccessToken = GenerateJwtToken(cliente);

        tokenExistente.Ativo = false;
        var novoRefreshToken = GenerateRefreshToken(cliente.Email);
        _context.RefreshTokens.Add(novoRefreshToken);
        _context.SaveChanges();

        return Ok(new
        {
            Token = novoAccessToken,
            RefreshToken = novoRefreshToken.Token
        });
    }

    private string GenerateJwtToken(Cliente cliente)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
            new Claim(ClaimTypes.Name, cliente.Nome),
            new Claim(ClaimTypes.Email, cliente.Email),
            new Claim(ClaimTypes.Role, "Cliente") // Diferencia clientes de usuários
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(string email)
    {
        return new RefreshToken
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            EmailUsuario = email,
            Expiration = DateTime.UtcNow.AddDays(7),
            Ativo = true
        };
    }
}