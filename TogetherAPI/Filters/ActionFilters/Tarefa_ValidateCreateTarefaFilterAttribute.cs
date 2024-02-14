using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ActionFilters;

public class Tarefa_ValidateCreateTarefaFilterAttribute : ActionFilterAttribute
{
    private readonly ITarefaRepository _tarefaRepository;

    public Tarefa_ValidateCreateTarefaFilterAttribute(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var tarefa = context.ActionArguments["tarefa"] as Tarefa;

        if (tarefa == null || tarefa.Nome == null)
        {
            context.ModelState.AddModelError("Tarefa", "Tarefa ou nome nulos.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };

            context.Result = new BadRequestObjectResult(problemDetails);
        }
        else
        {
            var tarefaExiste = _tarefaRepository.GetTarefaByNome(tarefa.Nome).Result;
            if (tarefaExiste != null)
            {
                context.ModelState.AddModelError("Tarefa", "Tarefa com mesmo nome existente.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
