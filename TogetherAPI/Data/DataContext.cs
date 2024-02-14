namespace TogetherAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Tarefa> Tarefas { get; set; }

}
