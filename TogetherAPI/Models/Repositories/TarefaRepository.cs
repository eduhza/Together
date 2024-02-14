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

    public async Task CreateTarefa(Tarefa tarefa)
    {
        try
        {
            //int maxId = _dataContext.Tarefas.Max(x => x.TarefaId);
            //tarefa.TarefaId = maxId + 1;
            _dataContext.Tarefas.Add(tarefa);
            
            await _dataContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            var a = ex.ToString();
        }
    }

    public async Task UpdateTarefa(Tarefa tarefa)
    {
        var tarefaAtualizar = await _dataContext.Tarefas.FirstAsync (x => x.TarefaId == tarefa.TarefaId);
        tarefaAtualizar.Descricao = tarefa.Descricao;
        tarefaAtualizar.Data = tarefa.Data;
        tarefaAtualizar.Nome = tarefa.Nome;
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
