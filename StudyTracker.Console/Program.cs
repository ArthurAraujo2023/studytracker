using System;
using StudyTracker.Core.DTOs;
using StudyTracker.Core.Models;
using StudyTracker.Core.Services;

SessaoEstudoService sessaoEstudoService = new SessaoEstudoService();

while (true)
{
    MostrarMenu();

    Console.Write("Opção: ");
    string textoOpcao = Console.ReadLine() ?? "";

    int opcaoConvertida;
    bool conversaoOpcao = int.TryParse(textoOpcao, out opcaoConvertida);

    if (conversaoOpcao == false)
    {
        Console.WriteLine("Erro: a opção precisa ser um número.");
        PausarTela();
        continue;
    }

    if (opcaoConvertida == 1)
    {
        CadastrarSessao(sessaoEstudoService);
    }
    else if (opcaoConvertida == 2)
    {
        ListarSessoes(sessaoEstudoService);
    }
    else if (opcaoConvertida == 3)
    {
        MostrarTotalDeSessoes(sessaoEstudoService);
    }
    else if (opcaoConvertida == 4)
    {
        BuscarSessaoPorId(sessaoEstudoService);
    }
    else if (opcaoConvertida == 5)
    {
        RemoverSessaoPorId(sessaoEstudoService);
    }
    else if (opcaoConvertida == 6)
    {
        AtualizarSessaoPorId(sessaoEstudoService);
    }
    else if (opcaoConvertida == 7)
    {
        BuscarSessoesPorMateria(sessaoEstudoService);
    }
    else if (opcaoConvertida == 0)
    {
        break;
    }
    else
    {
        Console.WriteLine("Opção inválida.");
        PausarTela();
    }

    Console.WriteLine();
}

void MostrarMenu()
{
    Console.WriteLine("Escolha uma das opções:");
    Console.WriteLine("1 - Criar sessão de estudo");
    Console.WriteLine("2 - Listar sessões");
    Console.WriteLine("3 - Ver total de sessões");
    Console.WriteLine("4 - Buscar sessão por Id");
    Console.WriteLine("5 - Remover sessão por Id");
    Console.WriteLine("6 - Atualizar sessão por Id");
    Console.WriteLine("7 - Buscar sessões por matéria");
    Console.WriteLine("0 - Sair");
}

void PausarTela()
{
    Console.WriteLine();
    Console.Write("Pressione Enter para voltar ao menu...");
    Console.ReadLine();
}

void MostrarSessao(SessaoEstudo sessao)
{
    Console.WriteLine("------------------------------");
    Console.WriteLine($"Id: {sessao.Id}");
    Console.WriteLine($"Matéria: {sessao.Materia}");
    Console.WriteLine($"Tópico: {sessao.Topico}");
    Console.WriteLine($"Minutos estudados: {sessao.MinutosEstudados}");
    Console.WriteLine($"Dificuldade: {sessao.Dificuldade}");
    Console.WriteLine($"Data da sessão: {sessao.DataSessao}");
    Console.WriteLine($"Concluído: {sessao.Concluido}");
    Console.WriteLine("------------------------------");
}

void CadastrarSessao(SessaoEstudoService service)
{
    CriarSessaoEstudoDTO criarSessaoEstudoDTO = new CriarSessaoEstudoDTO();

    Console.Write("Insira a Matéria: ");
    criarSessaoEstudoDTO.Materia = Console.ReadLine() ?? "";

    Console.Write("Insira o Tópico: ");
    criarSessaoEstudoDTO.Topico = Console.ReadLine() ?? "";

    Console.Write("Insira os Minutos estudados: ");
    string textoMinutos = Console.ReadLine() ?? "";

    int minutosConvertidos;
    bool minutosValido = int.TryParse(textoMinutos, out minutosConvertidos);

    if (minutosValido == false)
    {
        Console.WriteLine("Erro: os minutos precisam ser um número.");
        PausarTela();
        return;
    }

    criarSessaoEstudoDTO.MinutosEstudados = minutosConvertidos;

    Console.Write("Insira a Dificuldade: ");
    string textoDificuldade = Console.ReadLine() ?? "";

    int dificuldadeConvertida;
    bool dificuldadeValida = int.TryParse(textoDificuldade, out dificuldadeConvertida);

    if (dificuldadeValida == false)
    {
        Console.WriteLine("Erro: a dificuldade precisa ser um número.");
        PausarTela();
        return;
    }

    criarSessaoEstudoDTO.Dificuldade = dificuldadeConvertida;

    var resultado = service.CriarSessao(criarSessaoEstudoDTO);

    if (resultado.ErroValidacao)
    {
        Console.WriteLine(resultado.Mensagem);
    }
    else if (resultado.Sucesso && resultado.Dado != null)
    {
        Console.WriteLine(resultado.Mensagem);
        Console.WriteLine($"Id criado: {resultado.Dado.Id}");
    }
    PausarTela();
}

void ListarSessoes(SessaoEstudoService service)
{
    var sessoes = service.ListarSessoes();

    if (sessoes.Count == 0)
    {
        Console.WriteLine("Nenhuma sessão cadastrada.");
    }
    else
    {
        foreach (var sessao in sessoes)
        {
            MostrarSessao(sessao);
        }
    }

    PausarTela();
}

void MostrarTotalDeSessoes(SessaoEstudoService service)
{
    Console.WriteLine($"Total de sessões: {service.TotalDeSessoes}");

    PausarTela();
}

void BuscarSessaoPorId(SessaoEstudoService service)
{
    Console.Write("Insira o Id: ");
    string textoAConverter = Console.ReadLine() ?? "";

    int valorConvertido;
    bool valorAConverter = int.TryParse(textoAConverter, out valorConvertido);

    if (valorAConverter == false)
    {
        Console.WriteLine("Erro: o Id precisa ser um número.");
        PausarTela();
        return;
    }

    var sessaoEstudo = service.BuscarSessaoPorId(valorConvertido);

    if (sessaoEstudo == null)
    {
        Console.WriteLine("Sessão não encontrada.");
    }
    else
    {
        MostrarSessao(sessaoEstudo);
    }

    PausarTela();
}

void RemoverSessaoPorId(SessaoEstudoService service)
{
    Console.Write("Insira o Id para remover: ");
    string textoAConverter = Console.ReadLine() ?? "";

    int numeroConvertido;
    bool textoConvertendo = int.TryParse(textoAConverter, out numeroConvertido);

    if (textoConvertendo == false)
    {
        Console.WriteLine("Erro: o Id precisa ser um número.");
        PausarTela();
        return;
    }

    var resultado = service.RemoverSessaoPorId(numeroConvertido);

    if (resultado.NaoEncontrado)
    {
        Console.WriteLine(resultado.Mensagem);
    }
    else if (resultado.Sucesso)
    {
        Console.WriteLine(resultado.Mensagem);
    }

    PausarTela();
}

void AtualizarSessaoPorId(SessaoEstudoService service)
{
    AtualizarSessaoEstudoDTO atualizarSessaoEstudoDTO = new AtualizarSessaoEstudoDTO();

    Console.Write("Me envie o Id da sessão que será atualizada: ");
    string textoAConverter = Console.ReadLine() ?? "";

    int numero;
    bool textoConvertendo = int.TryParse(textoAConverter, out numero);

    if (textoConvertendo == false)
    {
        Console.WriteLine("Erro: o Id precisa ser um número.");
        PausarTela();
        return;
    }

    Console.Write("Insira a nova Matéria: ");
    atualizarSessaoEstudoDTO.Materia = Console.ReadLine() ?? "";

    Console.Write("Insira o novo Tópico: ");
    atualizarSessaoEstudoDTO.Topico = Console.ReadLine() ?? "";

    Console.Write("Insira os novos Minutos estudados: ");
    string textoMinutos = Console.ReadLine() ?? "";

    int minutosConvertidos;
    bool minutosValido = int.TryParse(textoMinutos, out minutosConvertidos);

    if (minutosValido == false)
    {
        Console.WriteLine("Erro: os minutos precisam ser um número.");
        PausarTela();
        return;
    }

    atualizarSessaoEstudoDTO.MinutosEstudados = minutosConvertidos;

    Console.Write("Insira a nova Dificuldade: ");
    string textoDificuldade = Console.ReadLine() ?? "";

    int dificuldadeConvertida;
    bool dificuldadeValida = int.TryParse(textoDificuldade, out dificuldadeConvertida);

    if (dificuldadeValida == false)
    {
        Console.WriteLine("Erro: a dificuldade precisa ser um número.");
        PausarTela();
        return;
    }

    atualizarSessaoEstudoDTO.Dificuldade = dificuldadeConvertida;

    var resultado = service.AtualizarSessaoPorId(numero, atualizarSessaoEstudoDTO);

    if (resultado.NaoEncontrado)
    {
        Console.WriteLine(resultado.Mensagem);
    }
    else if (resultado.ErroValidacao)
    {
        Console.WriteLine(resultado.Mensagem);
    }
    else if (resultado.Sucesso)
    {
        Console.WriteLine(resultado.Mensagem);
    }

    PausarTela();
}

void BuscarSessoesPorMateria(SessaoEstudoService service)
{
    Console.Write("Insira a matéria para buscar: ");
    string materia = Console.ReadLine() ?? "";

    var sessoes = service.BuscarSessoesPorMateria(materia);

    if (sessoes.Count == 0)
    {
        Console.WriteLine("Nenhuma sessão encontrada para essa matéria.");
    }
    else
    {
        foreach (var sessao in sessoes)
        {
            MostrarSessao(sessao);
        }
    }

    PausarTela();
}