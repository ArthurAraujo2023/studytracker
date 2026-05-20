namespace StudyTracker.Core.Results;

public class ResultadoOperacao
{
    public bool Sucesso { get; private set; }

    public string Mensagem { get; private set; } = string.Empty;

    public bool ErroValidacao { get; private set; }

    public bool NaoEncontrado { get; private set; }

    private ResultadoOperacao()
    {
    }

    public static ResultadoOperacao SucessoOperacao(string mensagem)
    {
        return new ResultadoOperacao
        {
            Sucesso = true,
            Mensagem = mensagem,
            ErroValidacao = false,
            NaoEncontrado = false
        };
    }

    public static ResultadoOperacao FalhaValidacao(string mensagem)
    {
        return new ResultadoOperacao
        {
            Sucesso = false,
            Mensagem = mensagem,
            ErroValidacao = true,
            NaoEncontrado = false
        };
    }

    public static ResultadoOperacao FalhaNaoEncontrado(string mensagem)
    {
        return new ResultadoOperacao
        {
            Sucesso = false,
            Mensagem = mensagem,
            ErroValidacao = false,
            NaoEncontrado = true
        };
    }
}