namespace StudyTracker.Core.DTOs;

public class AtualizarSessaoEstudoDTO
{
    public string Materia { get; set; } = string.Empty;

    public string Topico { get; set; } = string.Empty;

    public int MinutosEstudados { get; set; }

    public int Dificuldade { get; set; }
}