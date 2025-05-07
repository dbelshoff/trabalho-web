using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoX.Data;
using projetoX.Models;
using Microsoft.AspNetCore.Authorization;
using projetoX.DTOs;
using Newtonsoft.Json.Linq;
using System.Globalization;



namespace projetoX.Controllers;

[Route("api/enderecos")]
[ApiController]
public class EnderecoController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnderecoController(AppDbContext context)
    {
        _context = context;
    }



    [HttpGet("{id}")]
    public IActionResult ObterEndereco(int id)
    {
        var endereco = _context.Enderecos.Include(e => e.Empresa).FirstOrDefault(e => e.Id == id);
        if (endereco == null) return NotFound();

        var dto = new EnderecoDTO
        {
            Id = endereco.Id,
            Logradouro = endereco.Logradouro,
            Numero = endereco.Numero,
            Complemento = endereco.Complemento,
            Bairro = endereco.Bairro,
            Cidade = endereco.Cidade,
            Estado = endereco.Estado,
            CEP = endereco.CEP,
            Latitude = endereco.Latitude,
            Longitude = endereco.Longitude,
            EmpresaNome = endereco.Empresa?.Nome
        };

        return Ok(dto);
    }









[HttpGet("geolocalizacao")]
[AllowAnonymous]
public async Task<IActionResult> ObterEnderecoPorGeolocalizacao([FromQuery] double lat, [FromQuery] double lon)
{
    try
    {
        if (lat == 0 || lon == 0)
            return BadRequest("Coordenadas inválidas.");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "projetox");

        string url = $"https://nominatim.openstreetmap.org/reverse?lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&format=json";
        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Erro ao consultar serviço de geolocalização.");
        }

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);
        var address = json["address"];

        if (address == null)
        {
            return NotFound("Endereço não encontrado para essas coordenadas.");
        }

        var estadoNome = address?["state"]?.ToString() ?? "";
        var cidade = address?["city"]?.ToString()
                     ?? address?["town"]?.ToString()
                     ?? address?["village"]?.ToString()
                     ?? "";
        var bairro = address?["suburb"]?.ToString()
                     ?? address?["neighbourhood"]?.ToString()
                     ?? "";

        var estadoSigla = ObterSiglaEstado(estadoNome);

        return Ok(new
        {
            estado = estadoSigla,
            cidade,
            bairro
        });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro interno: {ex.Message}");
    }
}


private string ObterSiglaEstado(string nomeEstado)
{
    var estados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "Acre", "AC" },
        { "Alagoas", "AL" },
        { "Amapá", "AP" },
        { "Amazonas", "AM" },
        { "Bahia", "BA" },
        { "Ceará", "CE" },
        { "Distrito Federal", "DF" },
        { "Espírito Santo", "ES" },
        { "Goiás", "GO" },
        { "Maranhão", "MA" },
        { "Mato Grosso", "MT" },
        { "Mato Grosso do Sul", "MS" },
        { "Minas Gerais", "MG" },
        { "Pará", "PA" },
        { "Paraíba", "PB" },
        { "Paraná", "PR" },
        { "Pernambuco", "PE" },
        { "Piauí", "PI" },
        { "Rio de Janeiro", "RJ" },
        { "Rio Grande do Norte", "RN" },
        { "Rio Grande do Sul", "RS" },
        { "Rondônia", "RO" },
        { "Roraima", "RR" },
        { "Santa Catarina", "SC" },
        { "São Paulo", "SP" },
        { "Sergipe", "SE" },
        { "Tocantins", "TO" }
    };

    return estados.TryGetValue(nomeEstado ?? "", out var sigla) ? sigla : "";
}


    [HttpGet("por-empresa/{empresaId}")]
    [AllowAnonymous]
    public IActionResult ListarEnderecosPorEmpresa(int empresaId)
    {
        var enderecos = _context.Enderecos
            .Where(e => e.EmpresaId == empresaId)
            .Select(e => new EnderecoDTO
            {
                Id = e.Id,
                Logradouro = e.Logradouro,
                Numero = e.Numero,
                Complemento = e.Complemento,
                Bairro = e.Bairro,
                Cidade = e.Cidade,
                Estado = e.Estado,
                CEP = e.CEP,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                EmpresaNome = e.Empresa.Nome
            }).ToList();

        return Ok(enderecos);
    }


[HttpGet("bairros")]
[AllowAnonymous]
public IActionResult ListarBairrosPorCidade(
    [FromQuery] string estado,
    [FromQuery] string cidade)
{
    var bairros = _context.Enderecos
        .Where(e =>
            e.Estado.ToLower() == estado.ToLower() &&
            e.Cidade.ToLower() == cidade.ToLower() &&
            !string.IsNullOrEmpty(e.Bairro))
        .Select(e => e.Bairro)
        .Distinct()
        .OrderBy(e => e)
        .ToList();

    return Ok(bairros);
}


[HttpGet("cidades")]
[AllowAnonymous]
public IActionResult ListarCidadesPorEstado([FromQuery] string estado)
{
    var cidades = _context.Enderecos
        .Where(e => e.Estado.ToLower() == estado.ToLower() && !string.IsNullOrEmpty(e.Cidade))
        .Select(e => e.Cidade)
        .Distinct()
        .OrderBy(e => e)
        .ToList();

    return Ok(cidades);
}



[HttpGet("estados")]
[AllowAnonymous]
public IActionResult ListarEstados()
{
    var estados = _context.Enderecos
        .Select(e => e.Estado)
        .Where(e => !string.IsNullOrEmpty(e))
        .Distinct()
        .OrderBy(e => e)
        .ToList();

    return Ok(estados);
}


}

