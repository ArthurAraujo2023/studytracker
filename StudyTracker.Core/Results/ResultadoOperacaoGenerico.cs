namespace StudyTracker.Core.Results;

public class ResultadoOperacaoGenerico<T>
{
    public bool Sucesso { get; private set; }

    public string Mensagem { get; private set; } = string.Empty;

    public bool ErroValidacao { get; private set; }

    public bool NaoEncontrado { get; private set; }

    public T? Dado { get; private set; }

    private ResultadoOperacaoGenerico()
    {
    }

    public static ResultadoOperacaoGenerico<T> SucessoComDado(string mensagem, T dado)
    {
        return new ResultadoOperacaoGenerico<T>
        {
            Sucesso = true,
            Mensagem = mensagem,
            ErroValidacao = false,
            NaoEncontrado = false,
            Dado = dado
        };
    }

    public static ResultadoOperacaoGenerico<T> FalhaValidacao(string mensagem)
    {
        return new ResultadoOperacaoGenerico<T>
        {
            Sucesso = false,
            Mensagem = mensagem,
            ErroValidacao = true,
            NaoEncontrado = false,
            Dado = default
        };
    }

    public static ResultadoOperacaoGenerico<T> FalhaNaoEncontrado(string mensagem)
    {
        return new ResultadoOperacaoGenerico<T>
        {
            Sucesso = false,
            Mensagem = mensagem,
            ErroValidacao = false,
            NaoEncontrado = true,
            Dado = default
        };
    }
}
