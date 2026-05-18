using StudyTracker.Core.Models;
using StudyTracker.Core.DTOs;

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
        return listaDeSessoes;
    }

    public SessaoEstudo? CriarSessao(CriarSessaoEstudoDTO dto)
    {
        dto.Materia = dto.Materia.Trim();
        dto.Topico = dto.Topico.Trim();

        if (string.IsNullOrWhiteSpace(dto.Materia))
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(dto.Topico))
        {
            return null;
        }

        if (dto.MinutosEstudados <= 0)
        {
            return null;
        }

        if (dto.Dificuldade < 1 || dto.Dificuldade > 5)
        {
            return null;
        }

        SessaoEstudo sessaoEstudo = new SessaoEstudo()
        {
            Id = nextId,
            Materia = dto.Materia,
            Topico = dto.Topico,
            MinutosEstudados = dto.MinutosEstudados,
            Dificuldade = dto.Dificuldade,
            DataSessao = DateTime.Now,
            Concluido = true
        };

        listaDeSessoes.Add(sessaoEstudo);
        nextId++;

        return sessaoEstudo;
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
    public bool RemoverSessaoPorId(int id)
    {
        var sessao = BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return false;
        }

        listaDeSessoes.Remove(sessao);
        return true;
    }
    public bool AtualizarSessaoPorId(int id, AtualizarSessaoEstudoDTO dto)
    {
        var sessao = BuscarSessaoPorId(id);

        if (sessao == null)
        {
            return false;
        }

        dto.Materia = dto.Materia.Trim();
        dto.Topico = dto.Topico.Trim();

        if (string.IsNullOrWhiteSpace(dto.Materia))
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(dto.Topico))
        {
            return false;
        }

        if (dto.MinutosEstudados <= 0)
        {
            return false;
        }

        if (dto.Dificuldade < 1 || dto.Dificuldade > 5)
        {
            return false;
        }

        sessao.Materia = dto.Materia;
        sessao.Topico = dto.Topico;
        sessao.MinutosEstudados = dto.MinutosEstudados;
        sessao.Dificuldade = dto.Dificuldade;

        return true;
    }

}