using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ActionFilters;

public class Tarefa_ValidateTarefaIdFilterAttribute : ActionFilterAttribute
{
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var tarefaId = context.ActionArguments["id"] as int?;

        var tarefaRepository = context.HttpContext.RequestServices.GetRequiredService<ITarefaRepository>();

        if (tarefaId.HasValue)
        {
            if (tarefaId.Value <= 0)
            {
                context.ModelState.AddModelError("TarefaId", "TarefaId inválido.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
            else if (!await tarefaRepository.TarefaExiste(tarefaId.Value))
            {
                context.ModelState.AddModelError("TarefaId", "Tarefa não existe.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };

                context.Result = new NotFoundObjectResult(problemDetails);
            }
        }
    }
}
