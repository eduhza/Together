namespace TogetherAPI.Models.Validations;

public class Tarefa_EnsureFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var tarefa = validationContext.ObjectInstance as Tarefa;

        if(tarefa != null && !string.IsNullOrEmpty(tarefa.Data)) 
        {
            if (DateTime.TryParse(tarefa.Data, out DateTime data))
            {
                if(DateTime.Compare(data, DateTime.Now) <= 0)
                    return new ValidationResult("Erro, insira uma data no futuro.");
            }
            else
            {
                return new ValidationResult("Erro, não foi possível ler a data.");
            }
        }
        return ValidationResult.Success;
    }
}
