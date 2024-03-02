using Microsoft.AspNetCore.Mvc;

namespace TogetherAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    public ITarefaRepository _tarefaRepository { get; set; }
    public TarefaController(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTarefas()
    {
        return Ok(await _tarefaRepository.GetTarefas());
    }

    [HttpGet("{id}")]
    [Tarefa_ValidateTarefaIdFilter]
    public async Task<IActionResult> GetTarefaById(int id)
    {
        return Ok(await _tarefaRepository.GetTarefaById(id));
    }

    [HttpPost]
    [Tarefa_ValidateCreateTarefaFilter]
    public IActionResult CreateTarefa([FromBody] Tarefa tarefa)
    {
        var id = _tarefaRepository.CreateTarefa(tarefa).Result;

        return CreatedAtAction(nameof(GetTarefaById),
            new { id = id },
            tarefa);
    }

    [HttpPut("{id}")]
    [Tarefa_ValidateTarefaIdFilter]
    [Tarefa_ValidateUpdateTarefaFilter]
    [Tarefa_HandleUpdateExceptionsFilter]
    public IActionResult UpdateTarefaById(int id, Tarefa tarefa)
    {
        _tarefaRepository.UpdateTarefa(tarefa);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Tarefa_ValidateTarefaIdFilter]
    public IActionResult DeleteTarefaById(int id)
    {
        var tarefa = _tarefaRepository.GetTarefaById(id);

        _tarefaRepository.GetTarefaById(id);

        return Ok(tarefa);
    }
}
