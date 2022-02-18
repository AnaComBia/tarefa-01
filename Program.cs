using Tarefas.db;

bool sair = false;
while (!sair)
{
    string opcao = UI.SelecionaOpcaoEmMenu();

    switch (opcao)
    {
        case "L": ListarTodasAsTarefas(); break;
        case "P": ListarTarefasPendentes(); break;
        case "I": ListarTarefasPorId(); break;
        case "D": ListarTarefasPorDescricao(); break;
        case "N": IncluirNovaTarefa(); break;
        case "A": AlterarDescricaoDaTarefa(); break;
        case "C": ConcluirTarefa(); break;
        case "E": ExcluirTarefa(); break;

        case "S":
            sair = true;
            break;

        default:
            UI.ExibeErro("\nOpção não reconhecida.");
            break;
    }

    Console.Write("\nPressione uma tecla para continuar...");
    Console.ReadKey();
}

void ListarTodasAsTarefas()
{
    UI.ExibeDestaque("\n-- Listar todas as Tarefas ---");
    
    using (var _db = new tarefasContext())
    {
        var TodasTarefas = _db.Tarefa.ToList<Tarefa>();

        int quantidadeTarefas = TodasTarefas.Count();
        Console.WriteLine($"Temos: {quantidadeTarefas} tarefa(s)");

        foreach (var tarefa in TodasTarefas)

        {
            string feito = tarefa.Concluida ? "x"  : " ";
            Console.WriteLine($"\n[{feito}] {tarefa.Id} => {tarefa.Descricao}");
        }
        
    }
}

void ListarTarefasPendentes()
{
    UI.ExibeDestaque("\n-- Listar Tarefas Pendentes ---");

    using (var _db = new tarefasContext())
    {
        var TodasTarefas = _db.Tarefa.Where(t => !t.Concluida).ToList<Tarefa>();

        int quantidadeTarefas = TodasTarefas.Count();
        Console.WriteLine($"Temos: {quantidadeTarefas} tarefa(s)");

        foreach (var tarefa in TodasTarefas)

        {
            string feito = tarefa.Concluida ? "x"  : " ";
            Console.WriteLine($"\n[{feito}] {tarefa.Id} => {tarefa.Descricao}");
        }
    }
}

void ListarTarefasPorDescricao()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Descrição ---");
    string descricao = UI.SelecionaDescricao();

    using (var _db = new tarefasContext())
    {
        var TodasTarefas = _db.Tarefa.Where(t => t.Descricao.Contains(descricao))
        .OrderBy(t => t.Descricao)
        .ToList<Tarefa>();

        int quantidadeTarefas = TodasTarefas.Count();
        Console.WriteLine($"Temos: {quantidadeTarefas} tarefa(s)");

        foreach (var tarefa in TodasTarefas)

        {
            string feito = tarefa.Concluida ? "x"  : " ";
            Console.WriteLine($"\n[{feito}] {tarefa.Id} => {tarefa.Descricao}");
        }
    }
}

void ListarTarefasPorId()
{
    UI.ExibeDestaque("\n-- Listar Tarefas por Id ---");
    int id = UI.SelecionaId();
    
    using (var _db = new tarefasContext())
    {
        var tarefa = _db.Tarefa.Find(id);

        if (tarefa == null)
        {
            Console.WriteLine("Esse Id não diz respeito a nenhuma tarefa. ");
            return;
        }

        string feito = tarefa.Concluida ? "x"  : " ";
        Console.WriteLine($"\n[{feito}] {tarefa.Id} => {tarefa.Descricao}");
    }
}

void IncluirNovaTarefa()
{
    UI.ExibeDestaque("\n-- Incluir Nova Tarefa ---");
    string descricao = UI.SelecionaDescricao();
    
    if (String.IsNullOrEmpty(descricao))
    {
        Console.WriteLine("Preciso de uma descrição para cadrastar a tarefa");
        return;
    }

    using (var _db = new tarefasContext())
    {
        var tarefa = new Tarefa {
            Descricao = descricao
        };

        _db.Tarefa.Add(tarefa);
        _db.SaveChanges();

        string feito = tarefa.Concluida ? "x"  : " ";
        Console.WriteLine($"\n[{feito}] {tarefa.Id} => {tarefa.Descricao}");
    }

}

void AlterarDescricaoDaTarefa()
{
    UI.ExibeDestaque("\n-- Alterar Descrição da Tarefa ---");
    int id = UI.SelecionaId();
    string descricao = UI.SelecionaDescricao();
    // Continue daqui
    Console.WriteLine(id);
    Console.WriteLine(descricao);
}

void ConcluirTarefa()
{
    UI.ExibeDestaque("\n-- Concluir Tarefa ---");
    int id = UI.SelecionaId();
    // Continue daqui
    Console.WriteLine(id);
}

void ExcluirTarefa()
{
    UI.ExibeDestaque("\n-- Excluir Tarefa ---");
    int id = UI.SelecionaId();
    // Continue daqui
    Console.WriteLine(id);
}
