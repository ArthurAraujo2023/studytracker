namespace StudyTracker.Core.Models;

public class SessaoEstudo
{
    public int Id { get; set; }

    public string Materia { get; set; } = string.Empty;

    public string Topico { get; set; } = string.Empty;

    public int MinutosEstudados { get; set; }

    public int Dificuldade { get; set; }

    public DateTime DataSessao { get; set; }

    public bool Concluido { get; set; }
}