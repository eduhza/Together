using System.ComponentModel.DataAnnotations;

namespace TogetherAPI.Models.Entities;

public class Tarefa
{
    public int TarefaId { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório!")]
    [MaxLength(100)]
    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    [Tarefa_EnsureFutureDate]
    public string? Data { get; set; }
}
