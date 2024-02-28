namespace TogetherAPI.Models.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly DataContext _dataContext;

    public TarefaRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Tarefa>> GetTarefas() => await _dataContext.Tarefas.ToListAsync(); //tarefas;

    public async Task<Tarefa?> GetTarefaById(int id) => await _dataContext.Tarefas.FirstOrDefaultAsync(x => x.TarefaId == id);

    public async Task<Tarefa?> GetTarefaByNome(string nome)
    {
        return await _dataContext.Tarefas.FirstOrDefaultAsync(x =>
                !string.IsNullOrEmpty(x.Nome) &&
                !string.IsNullOrEmpty(nome) &&
                x.Nome.ToLower() == nome.ToLower());
    }

    public async Task<int?> CreateTarefa(Tarefa tarefa)
    {
        _dataContext.Tarefas.Add(tarefa);
        await _dataContext.SaveChangesAsync();

        return tarefa.Nome != null ? await _dataContext.Tarefas.CountAsync() : 0;
    }

    public async Task UpdateTarefa(Tarefa tarefa)
    {
        var tarefaAtualizar = await _dataContext.Tarefas.FirstAsync(x => x.TarefaId == tarefa.TarefaId);
        if(tarefaAtualizar  != null)
        {
            tarefaAtualizar.Descricao = tarefa.Descricao;
            tarefaAtualizar.Data = tarefa.Data;
            tarefaAtualizar.Nome = tarefa.Nome;

            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task<bool> TarefaExiste(int? id) 
    { 
        var exist = await _dataContext.Tarefas.FindAsync(id);
        if (exist == null) return false;
        return true;
    }

    public async Task DeleteTarefa(Tarefa tarefa)
    {
        _dataContext.Tarefas.Remove(tarefa);
        await _dataContext.SaveChangesAsync();
    }
}
