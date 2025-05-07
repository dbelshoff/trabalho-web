using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoX.Data;
using projetoX.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using projetoX.DTOs;

namespace projetoX.Controllers;

[Route("api/empresas")]
[ApiController]
public class EmpresaController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpresaController(AppDbContext context)
    {
        _context = context;
    }





[HttpGet("filtrar-completo")]
[AllowAnonymous]
public IActionResult FiltrarEmpresasPorCategoriaELocalizacao(
    [FromQuery] string? categoria,
    [FromQuery] string? estado,
    [FromQuery] string? cidade,
    [FromQuery] string? bairro)
{
    try
    {
        // Verifica se o estado foi informado
        if (string.IsNullOrWhiteSpace(estado))
        {
            return BadRequest("O campo 'estado' é obrigatório.");
        }

        var query = _context.Empresas
            .Include(e => e.Categorias)
            .Include(e => e.Enderecos)
            .AsQueryable();

        // Filtrar por categoria, se informada
        if (!string.IsNullOrWhiteSpace(categoria))
        {
            query = query.Where(e => e.Categorias.Any(c => c.Nome == categoria));
        }

        // Estado é obrigatório, então sempre será aplicado
        query = query.Where(e => e.Enderecos.Any(end => end.Estado.Contains(estado)));

        // Filtrar por cidade, se informada
        if (!string.IsNullOrWhiteSpace(cidade))
        {
            query = query.Where(e => e.Enderecos.Any(end => end.Cidade.Contains(cidade)));
        }

        // Filtrar por bairro, se informado
        if (!string.IsNullOrWhiteSpace(bairro))
        {
            query = query.Where(e => e.Enderecos.Any(end => end.Bairro.Contains(bairro)));
        }

        var empresas = query.ToList();

        var empresasDTO = empresas.Select(e => new EmpresaDTO
        {
            Id = e.Id,
            Nome = e.Nome,
            CNPJ = e.CNPJ,
            Telefone = e.Telefone,
            Instagram = e.Instagram,
            Site = e.Site,
            Requerido = e.Requerido,
            Nivel = e.Nivel,
            Categorias = e.Categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList(),
            Enderecos = e.Enderecos.Select(end => new EnderecoDTO
            {
                Id = end.Id,
                Logradouro = end.Logradouro,
                Numero = end.Numero,
                Complemento = end.Complemento,
                Bairro = end.Bairro,
                Cidade = end.Cidade,
                Estado = end.Estado,
                CEP = end.CEP,
                Latitude = end.Latitude,
                Longitude = end.Longitude,
                EmpresaNome = e.Nome
            }).ToList()
        }).ToList();

        return Ok(empresasDTO);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Erro ao filtrar empresas.");
    }
}
}
