using MSilvaPizza.Models;
using MSilvaPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace MSilvaPizza.Controllers;

[ApiController]
[Route("[controller]")]

public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;
    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll()
    {
        return await _pizzaService.GetAll();
    }

    [HttpGet("{uuid}")]
    public async Task<ActionResult<Pizza>> Get(Guid uuid)
    {
        var pizza = await _pizzaService.GetById(uuid);
        if (pizza is null)
        {
            return NotFound();
        }
        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza pizza)
    {
        var newPizza = await _pizzaService.Create(pizza);
        return CreatedAtAction(nameof(Get), new { uuid = newPizza.Uuid }, newPizza);
    }

    [HttpPatch("{uuid}")]
    public async Task<IActionResult> Update(Guid uuid, Pizza pizza)
    {
        if (uuid != pizza.Uuid)
            return BadRequest();

        var updated = await _pizzaService.Update(uuid, pizza);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{uuid}")]
    public async Task<IActionResult> Delete(Guid uuid)
    {
        var deleted = await _pizzaService.Delete(uuid);

        if (!deleted) return NotFound();

        return NoContent();
    }
}