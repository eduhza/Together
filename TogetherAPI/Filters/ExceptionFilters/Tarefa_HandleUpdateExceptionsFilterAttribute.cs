using Microsoft.AspNetCore.Mvc.Filters;

namespace TogetherAPI.Filters.ExceptionFilters;

public class Tarefa_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
{
    //Só vai disparar se a tarefa for excluída logo após chamar o método
    public override void OnException(ExceptionContext context)
    {
        base.OnException(context);

        var tarefaId = context.RouteData.Values["id"] as int?;

        var tarefaRepository = context.HttpContext.RequestServices.GetRequiredService<ITarefaRepository>();

        if (!tarefaRepository.TarefaExiste(tarefaId).Result)
        {
            context.ModelState.AddModelError("TarefaId", "TarefaId não existe mais no repositório.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(problemDetails);
        }
    }
}
