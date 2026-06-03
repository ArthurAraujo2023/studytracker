using StudyTracker.Core.DTOs;
using StudyTracker.Core.Interfaces;
using StudyTracker.Core.Models;
using StudyTracker.Core.Results;

namespace StudyTracker.Core.Services;

public class SessaoEstudoService
{
    private readonly ISessaoEstudoRepository _repository;

    public SessaoEstudoService(ISessaoEstudoRepository repository)
    {
        _repository = repository;
    }

    public int TotalDeSessoes
    {
        get { return _repository.ListarTodas().Count; }
    }

    public List<SessaoEstudo> ListarSessoes()
    {
        return _repository.ListarTodas();
    }

    public ResultadoOperacaoGenerico<SessaoEstudo> CriarSessao(CriarSessaoEstudoDTO dto)
    {
        string materia = dto.Materia.Trim();
        string topico = dto.Topico.Trim();

        if (string.IsNullOrWhiteSpace(materia))
        {
            return ResultadoOperacaoGenerico<SessaoEstudo>.FalhaValidacao("Matéria é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(topico))
        {
            return ResultadoOperacaoGenerico<SessaoEstudo>.FalhaValidacao("Tópico é obrigatório.");
        }

        if (dto.MinutosEstudados <= 0)
        {
            return ResultadoOperacaoGenerico<SessaoEstudo>.FalhaValidacao("Minutos estudados devem ser maiores que zero.");
        }

        if (dto.Dificuldade < 1 || dto.Dificuldade > 5)
        {
            return ResultadoOperacaoGenerico<SessaoEstudo>.FalhaValidacao("Dificuldade deve estar entre 1 e 5.");
        }

        SessaoEstudo sessaoEstudo = new SessaoEstudo()
        {
            Materia = materia,
            Topico = topico,
            MinutosEstudados = dto.MinutosEstudados,
            Dificuldade = dto.Dificuldade,
            DataSessao = DateTime.Now,
            Concluido = true
        };

        _repository.Adicionar(sessaoEstudo);

        return ResultadoOperacaoGenerico<SessaoEstudo>.SucessoComDado(
            "Sessão criada com sucesso.",
            sessaoEstudo
        );
    }

    public SessaoEstudo? BuscarSessaoPorId(int id)
    {
        return _repository.BuscarPorId(id);
    }

    public ResultadoOperacao RemoverSessaoPorId(int id)
    {
        var sessao = BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return ResultadoOperacao.FalhaNaoEncontrado("Sessão não encontrada.");
        }

        _repository.Remover(sessao);

        return ResultadoOperacao.SucessoOperacao("Sessão removida com sucesso.");
    }

    public ResultadoOperacao AtualizarSessaoPorId(int id, AtualizarSessaoEstudoDTO dto)
    {
        var sessao = BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return ResultadoOperacao.FalhaNaoEncontrado("Sessão não encontrada.");
        }

        string materia = dto.Materia.Trim();
        string topico = dto.Topico.Trim();

        if (string.IsNullOrWhiteSpace(materia))
        {
            return ResultadoOperacao.FalhaValidacao("Matéria é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(topico))
        {
            return ResultadoOperacao.FalhaValidacao("Tópico é obrigatório.");
        }

        if (dto.MinutosEstudados <= 0)
        {
            return ResultadoOperacao.FalhaValidacao("Minutos estudados devem ser maiores que zero.");
        }

        if (dto.Dificuldade < 1 || dto.Dificuldade > 5)
        {
            return ResultadoOperacao.FalhaValidacao("Dificuldade deve estar entre 1 e 5.");
        }

        sessao.Materia = materia;
        sessao.Topico = topico;
        sessao.MinutosEstudados = dto.MinutosEstudados;
        sessao.Dificuldade = dto.Dificuldade;

        _repository.Atualizar(sessao);

        return ResultadoOperacao.SucessoOperacao("Sessão atualizada com sucesso.");
    }

    public List<SessaoEstudo> BuscarSessoesPorMateria(string materia)
    {
        if (string.IsNullOrWhiteSpace(materia))
        {
            return new List<SessaoEstudo>();
        }

        string materiaLimpa = materia.Trim();

        return _repository.BuscarPorMateria(materiaLimpa);
    }
}