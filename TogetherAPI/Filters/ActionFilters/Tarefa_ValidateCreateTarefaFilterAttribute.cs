using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ActionFilters;

public class Tarefa_ValidateCreateTarefaFilterAttribute : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var tarefaRepository = context.HttpContext.RequestServices.GetRequiredService<ITarefaRepository>();

        if (context.ActionArguments["tarefa"] is not Tarefa tarefa || tarefa.Nome == null)
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
            var tarefaExiste = await tarefaRepository.GetTarefaByNome(tarefa.Nome);
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
