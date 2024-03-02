using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ActionFilters;

public class Tarefa_ValidateCreateTarefaFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var tarefa = context.ActionArguments["tarefa"] as Tarefa;

        var tarefaRepository = context.HttpContext.RequestServices.GetRequiredService<ITarefaRepository>();

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
            var tarefaExiste = tarefaRepository.GetTarefaByNome(tarefa.Nome).Result;
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
