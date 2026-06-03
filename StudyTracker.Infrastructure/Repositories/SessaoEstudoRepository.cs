using StudyTracker.Core.Interfaces;
using StudyTracker.Core.Models;
using StudyTracker.Infrastructure.Data;

namespace StudyTracker.Infrastructure.Repositories;

public class SessaoEstudoRepository : ISessaoEstudoRepository
{
    private readonly StudyTrackerDbContext _dbContext;

    public SessaoEstudoRepository(StudyTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<SessaoEstudo> ListarTodas()
    {
        return _dbContext.SessoesEstudo.ToList();
    }

    public SessaoEstudo? BuscarPorId(int id)
    {
        return _dbContext.SessoesEstudo.Find(id);
    }

    public List<SessaoEstudo> BuscarPorMateria(string materia)
    {
        string materiaLimpa = materia.Trim().ToLower();

        return _dbContext.SessoesEstudo
            .Where(sessao => sessao.Materia.ToLower() == materiaLimpa)
            .ToList();
    }

    public void Adicionar(SessaoEstudo sessao)
    {
        _dbContext.SessoesEstudo.Add(sessao);
        _dbContext.SaveChanges();
    }

    public void Atualizar(SessaoEstudo sessao)
    {
        _dbContext.SessoesEstudo.Update(sessao);
        _dbContext.SaveChanges();
    }

    public void Remover(SessaoEstudo sessao)
    {
        _dbContext.SessoesEstudo.Remove(sessao);
        _dbContext.SaveChanges();
    }
}