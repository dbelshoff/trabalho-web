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



    [HttpGet]
public IActionResult ListarEmpresas()
{
    var empresas = _context.Empresas.Include(e => e.Categorias).ToList();

    var empresasDTO = empresas.Select(e => new EmpresaDTO
    {
        Id = e.Id,
        Nome = e.Nome,
        CNPJ = e.CNPJ,
        Telefone = e.Telefone,
        Instagram = e.Instagram,
        Site = e.Site,
        Categorias = e.Categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList()
    }).ToList();

    return Ok(empresasDTO);
}


    [HttpGet("{id}")]
public IActionResult ObterEmpresa(int id)
{
    var empresa = _context.Empresas.Include(e => e.Categorias).FirstOrDefault(e => e.Id == id);

    if (empresa == null) return NotFound();

    var empresaDTO = new EmpresaDTO
    {
        Id = empresa.Id,
        Nome = empresa.Nome,
        CNPJ = empresa.CNPJ,
        Telefone = empresa.Telefone,
        Instagram = empresa.Instagram,
        Site = empresa.Site,
        Categorias = empresa.Categorias.Select(c => new CategoriaDTO
        {
            Id = c.Id,
            Nome = c.Nome
        }).ToList()
    };

    return Ok(empresaDTO);
}




[HttpGet("por-categoria")]
[AllowAnonymous]
public IActionResult ListarEmpresasPorCategoria([FromQuery] string categoria)
{
    try
    {
        var empresas = _context.Empresas
            .Include(e => e.Categorias)
            .Where(e => e.Categorias.Any(c => EF.Functions.Like(c.Nome, $"%{categoria}%")))
            .ToList();

        var empresasDTO = empresas.Select(e => new EmpresaDTO
        {
            Id = e.Id,
            Nome = e.Nome,
            CNPJ = e.CNPJ,
            Telefone = e.Telefone,
            Instagram = e.Instagram,
            Site = e.Site,
            Categorias = e.Categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList()
        }).ToList();

        return Ok(empresasDTO);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Ocorreu um erro interno ao processar a requisição.");
    }
}



[HttpGet("por-endereco")]
[AllowAnonymous]
public IActionResult ListarEmpresasPorEndereco(
    [FromQuery] string? bairro, 
    [FromQuery] string? cidade, 
    [FromQuery] string estado)
{
    try
    {
        // ⚠️ Verifica se o estado foi informado
        if (string.IsNullOrWhiteSpace(estado))
        {
            return BadRequest("O campo 'estado' é obrigatório.");
        }

        var query = _context.Empresas
            .Include(e => e.Categorias)
            .Include(e => e.Enderecos)
            .AsQueryable();

        // ✅ Aplica o filtro por estado (obrigatório)
        query = query.Where(e => e.Enderecos.Any(end =>
            !string.IsNullOrEmpty(end.Estado) && end.Estado.Contains(estado)));

        // ✅ Aplica o filtro por cidade (opcional)
        if (!string.IsNullOrWhiteSpace(cidade))
        {
            query = query.Where(e => e.Enderecos.Any(end =>
                !string.IsNullOrEmpty(end.Cidade) && end.Cidade.Contains(cidade)));
        }

        // ✅ Aplica o filtro por bairro (opcional)
        if (!string.IsNullOrWhiteSpace(bairro))
        {
            query = query.Where(e => e.Enderecos.Any(end =>
                !string.IsNullOrEmpty(end.Bairro) && end.Bairro.Contains(bairro)));
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
        // Logue o erro se desejar
        return StatusCode(500, "Erro interno ao buscar empresas por endereço.");
    }
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
