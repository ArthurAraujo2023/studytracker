using StudyTracker.Core.Models;

namespace StudyTracker.Core.Interfaces;

public interface ISessaoEstudoRepository
{
    List<SessaoEstudo> ListarTodas();

    SessaoEstudo? BuscarPorId(int id);

    List<SessaoEstudo> BuscarPorMateria(string materia);

    void Adicionar(SessaoEstudo sessao);

    void Atualizar(SessaoEstudo sessao);

    void Remover(SessaoEstudo sessao);
}