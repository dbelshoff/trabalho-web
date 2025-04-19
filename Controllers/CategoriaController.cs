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

[Route("api/categorias")]
[Authorize]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriaController(AppDbContext context)
    {
        _context = context;
    }

   

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ListarCategorias()
    {
        var categorias = _context.Categorias
            .Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();

        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public IActionResult ObterCategoria(int id)
    {
        var categoria = _context.Categorias
            .Where(c => c.Id == id)
            .Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).FirstOrDefault();

        if (categoria == null) return NotFound();
        return Ok(categoria);
    }
}
