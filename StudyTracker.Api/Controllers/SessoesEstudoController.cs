using Microsoft.AspNetCore.Mvc;
using StudyTracker.Core.DTOs;
using StudyTracker.Core.Models;
using StudyTracker.Core.Services;

namespace StudyTracker.Api.Controllers;

[ApiController]
[Route("api/sessoes-estudo")]
public class SessoesEstudoController : ControllerBase
{
    private readonly SessaoEstudoService sessaoEstudoService;

    public SessoesEstudoController(SessaoEstudoService service)
    {
        sessaoEstudoService = service;
    }

    [HttpGet]
    public ActionResult<List<SessaoEstudo>> ListarSessoes()
    {
        var sessoes = sessaoEstudoService.ListarSessoes();

        return Ok(sessoes);
    }

    [HttpGet("buscar")]
    public ActionResult<List<SessaoEstudo>> BuscarSessoesPorMateria([FromQuery] string materia)
    {
        var sessoes = sessaoEstudoService.BuscarSessoesPorMateria(materia);

        return Ok(sessoes);
    }

    [HttpGet("{id:int}")]
    public ActionResult<SessaoEstudo> BuscarSessaoPorId([FromRoute] int id)
    {
        var sessao = sessaoEstudoService.BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return NotFound();
        }

        return Ok(sessao);
    }

    [HttpPost]
    public ActionResult<SessaoEstudo> CriarSessao([FromBody] CriarSessaoEstudoDTO dto)
    {
        var sessaoCriada = sessaoEstudoService.CriarSessao(dto);

        if (sessaoCriada == null)
        {
            return BadRequest("Dados inválidos.");
        }

        return CreatedAtAction(
            nameof(BuscarSessaoPorId),
            new { id = sessaoCriada.Id },
            sessaoCriada
        );
    }

    [HttpPut("{id:int}")]
    public ActionResult AtualizarSessaoPorId([FromRoute] int id, [FromBody] AtualizarSessaoEstudoDTO dto)
    {
        var resultado = sessaoEstudoService.AtualizarSessaoPorId(id, dto);

        if (resultado.NaoEncontrado)
        {
            return NotFound(resultado.Mensagem);
        }

        if (resultado.ErroValidacao)
        {
            return BadRequest(resultado.Mensagem);
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult RemoverSessaoPorId([FromRoute] int id)
    {
        var resultado = sessaoEstudoService.RemoverSessaoPorId(id);

        if (resultado.NaoEncontrado)
        {
            return NotFound(resultado.Mensagem);
        }

        return NoContent();
    }
}