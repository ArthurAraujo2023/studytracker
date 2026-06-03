using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudyTracker.Core.DTOs;
using StudyTracker.Core.Interfaces;
using StudyTracker.Core.Models;
using StudyTracker.Core.Services;
using StudyTracker.Infrastructure.Data;
using StudyTracker.Infrastructure.Repositories;

string databasePath = Path.GetFullPath(
    Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "database", "studytracker.db")
);

Directory.CreateDirectory(Path.GetDirectoryName(databasePath)!);

ServiceCollection services = new ServiceCollection();

services.AddDbContext<StudyTrackerDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

services.AddScoped<ISessaoEstudoRepository, SessaoEstudoRepository>();
services.AddScoped<SessaoEstudoRepository>();
services.AddScoped<SessaoEstudoService>();

using ServiceProvider serviceProvider = services.BuildServiceProvider();
using IServiceScope scope = serviceProvider.CreateScope();

SessaoEstudoService sessaoEstudoService =
    scope.ServiceProvider.GetRequiredService<SessaoEstudoService>();

Console.WriteLine("StudyTracker Console iniciado.");
Console.WriteLine($"Banco usado: {databasePath}");
Console.WriteLine();

while (true)
{
    MostrarMenu();

    Console.Write("Opção: ");
    string textoOpcao = Console.ReadLine() ?? "";

    bool conversaoOpcao = int.TryParse(textoOpcao, out int opcaoConvertida);

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
        Console.WriteLine("Encerrando o Console...");
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
    Console.Clear();
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

    bool minutosValido = int.TryParse(textoMinutos, out int minutosConvertidos);

    if (minutosValido == false)
    {
        Console.WriteLine("Erro: os minutos precisam ser um número.");
        PausarTela();
        return;
    }

    criarSessaoEstudoDTO.MinutosEstudados = minutosConvertidos;

    Console.Write("Insira a Dificuldade: ");
    string textoDificuldade = Console.ReadLine() ?? "";

    bool dificuldadeValida = int.TryParse(textoDificuldade, out int dificuldadeConvertida);

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
    else
    {
        Console.WriteLine("Não foi possível criar a sessão.");
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
    var sessoes = service.ListarSessoes();

    Console.WriteLine($"Total de sessões: {sessoes.Count}");

    PausarTela();
}

void BuscarSessaoPorId(SessaoEstudoService service)
{
    Console.Write("Insira o Id: ");
    string textoAConverter = Console.ReadLine() ?? "";

    bool valorAConverter = int.TryParse(textoAConverter, out int valorConvertido);

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

    bool textoConvertendo = int.TryParse(textoAConverter, out int numeroConvertido);

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
    else
    {
        Console.WriteLine("Não foi possível remover a sessão.");
    }

    PausarTela();
}

void AtualizarSessaoPorId(SessaoEstudoService service)
{
    AtualizarSessaoEstudoDTO atualizarSessaoEstudoDTO = new AtualizarSessaoEstudoDTO();

    Console.Write("Me envie o Id da sessão que será atualizada: ");
    string textoAConverter = Console.ReadLine() ?? "";

    bool textoConvertendo = int.TryParse(textoAConverter, out int numero);

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

    bool minutosValido = int.TryParse(textoMinutos, out int minutosConvertidos);

    if (minutosValido == false)
    {
        Console.WriteLine("Erro: os minutos precisam ser um número.");
        PausarTela();
        return;
    }

    atualizarSessaoEstudoDTO.MinutosEstudados = minutosConvertidos;

    Console.Write("Insira a nova Dificuldade: ");
    string textoDificuldade = Console.ReadLine() ?? "";

    bool dificuldadeValida = int.TryParse(textoDificuldade, out int dificuldadeConvertida);

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
    else
    {
        Console.WriteLine("Não foi possível atualizar a sessão.");
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