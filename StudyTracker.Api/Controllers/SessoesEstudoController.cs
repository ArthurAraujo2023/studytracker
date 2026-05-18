using Microsoft.AspNetCore.Mvc;
using StudyTracker.Core.Services;
using StudyTracker.Core.Models;
using StudyTracker.Core.DTOs;

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


    [HttpGet("{id}")]
    public ActionResult<SessaoEstudo> BuscarSessaoPorId([FromRoute] int id)
    {
        var sessao = sessaoEstudoService.BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return NotFound();
        }

        return Ok(sessao);
    }

    [HttpDelete("{id}")]
    public ActionResult RemoverSessaoPorId([FromRoute] int id)
    {
        bool removido = sessaoEstudoService.RemoverSessaoPorId(id);

        if (removido == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]

    public ActionResult AtualizarSessaoPorId([FromRoute] int id, [FromBody] AtualizarSessaoEstudoDTO dto)
    {
        var atualizar = sessaoEstudoService.AtualizarSessaoPorId(id, dto);

        if (atualizar == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("buscar")]
    public ActionResult<List<SessaoEstudo>> BuscarSessoesPorMateria([FromQuery] string materia)
    {
        var listaDeMateria = sessaoEstudoService.BuscarSessoesPorMateria(materia);

        return Ok(listaDeMateria);
    }


}