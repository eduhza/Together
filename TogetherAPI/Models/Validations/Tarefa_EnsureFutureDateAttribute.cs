namespace TogetherAPI.Models.Validations;

public class Tarefa_EnsureFutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var tarefa = validationContext.ObjectInstance as Tarefa;

        if(tarefa != null && !string.IsNullOrEmpty(tarefa.Data)) 
        {
            if (DateTime.Compare(DateTime.ParseExact(tarefa.Data, "dd/MM/yyyy HH:mm", null), DateTime.Now) <= 0)
            {
                return new ValidationResult("Erro, insira uma data no futuro.");
            }
        }
        return ValidationResult.Success;
    }
}
