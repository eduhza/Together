using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ActionFilters;

public class Tarefa_ValidateUpdateTarefaFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var id = context.ActionArguments["id"] as int?;

        if (id.HasValue && context.ActionArguments["tarefa"] is Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                context.ModelState.AddModelError("TarefaId", "TarefaId diferente do id.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
