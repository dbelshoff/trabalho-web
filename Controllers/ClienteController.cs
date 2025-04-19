using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoX.Data;
using projetoX.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace projetoX.Controllers;

[Route("api/clientes")]
//[Authorize]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CriarCliente([FromBody] Cliente cliente)
    {
        cliente.SenhaHash = BCrypt.Net.BCrypt.HashPassword(cliente.SenhaHash);
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObterCliente), new { id = cliente.Id }, cliente);
    }

    [HttpGet("{id}")]
    public IActionResult ObterCliente(int id)
    {
        var cliente = _context.Clientes.Find(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }
}
