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

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _pizzaService.GetById(id);
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
        return CreatedAtAction(nameof(Get), new { id = newPizza.Id }, newPizza);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();

        var updated = await _pizzaService.Update(id, pizza);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pizzaService.Delete(id);

        if (!deleted) return NotFound();

        return NoContent();
    }
}