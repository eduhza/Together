namespace TogetherAPI.Models.Interfaces;

public interface ITarefaRepository
{
    Task<List<Tarefa>> GetTarefas();
    Task<Tarefa?> GetTarefaById(int id);
    Task<Tarefa?> GetTarefaByNome(string nome);
    Task CreateTarefa(Tarefa tarefa);
    Task UpdateTarefa(Tarefa tarefa);
    Task DeleteTarefa(Tarefa tarefa);
    Task<bool> TarefaExiste(int? id);
}
