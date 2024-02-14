﻿namespace TogetherAPI.Models.Entities;

public class Tarefa
{
    public int TarefaId { get; set; }

    [Required]
    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    [Tarefa_EnsureFutureDate]
    public string? Data { get; set; }
}