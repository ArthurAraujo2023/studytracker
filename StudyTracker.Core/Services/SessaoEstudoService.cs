using StudyTracker.Core.DTOs;
using StudyTracker.Core.Models;
using StudyTracker.Core.Results;

namespace StudyTracker.Core.Services;

public class SessaoEstudoService
{
    private readonly List<SessaoEstudo> listaDeSessoes = new List<SessaoEstudo>();
    private int nextId = 1;

    public int TotalDeSessoes
    {
        get { return listaDeSessoes.Count; }
    }

    public List<SessaoEstudo> ListarSessoes()
    {
        return new List<SessaoEstudo>(listaDeSessoes);
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
            Id = nextId,
            Materia = materia,
            Topico = topico,
            MinutosEstudados = dto.MinutosEstudados,
            Dificuldade = dto.Dificuldade,
            DataSessao = DateTime.Now,
            Concluido = true
        };

        listaDeSessoes.Add(sessaoEstudo);
        nextId++;

        return ResultadoOperacaoGenerico<SessaoEstudo>.SucessoComDado(
            "Sessão criada com sucesso.",
            sessaoEstudo
        );
    }
    public SessaoEstudo? BuscarSessaoPorId(int id)
    {
        foreach (var sessao in listaDeSessoes)
        {
            if (sessao.Id == id)
            {
                return sessao;
            }
        }

        return null;
    }

    public ResultadoOperacao RemoverSessaoPorId(int id)
    {
        var sessao = BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return ResultadoOperacao.FalhaNaoEncontrado("Sessão não encontrada.");
        }

        listaDeSessoes.Remove(sessao);

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

        return ResultadoOperacao.SucessoOperacao("Sessão atualizada com sucesso.");
    }

    public List<SessaoEstudo> BuscarSessoesPorMateria(string materia)
    {
        List<SessaoEstudo> resultado = new List<SessaoEstudo>();

        if (string.IsNullOrWhiteSpace(materia))
        {
            return resultado;
        }

        string materiaLimpa = materia.Trim();

        foreach (var sessao in listaDeSessoes)
        {
            if (string.Equals(sessao.Materia, materiaLimpa, StringComparison.OrdinalIgnoreCase))
            {
                resultado.Add(sessao);
            }
        }

        return resultado;
    }
}