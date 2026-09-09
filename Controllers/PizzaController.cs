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
    public async Task<IActionResult> Update(Guid uuid, [FromBody] UpdatePizzaDto patch)
    {
        var existingPizza = await _pizzaService.GetById(uuid);
        if (existingPizza is null)
            return NotFound();

        if (patch.Name != null)
            existingPizza.Name = patch.Name;

        if (patch.IsGlutenFree != null)
            existingPizza.IsGlutenFree = patch.IsGlutenFree.HasValue ? patch.IsGlutenFree.Value : existingPizza.IsGlutenFree;

        if (patch.Description != null)
            existingPizza.Description = patch.Description;

        if (patch.Width != null)
            existingPizza.Width = patch.Width;

        if (patch.DoughType != null)
            existingPizza.DoughType = patch.DoughType;

        if (patch.Ingredients != null)
            existingPizza.Ingredients = patch.Ingredients;

        if (patch.Price != null)
            existingPizza.Price = patch.Price.HasValue ? patch.Price.Value : existingPizza.Price;

        if (patch.ImageUrl != null)
            existingPizza.ImageUrl = patch.ImageUrl;

        await _pizzaService.Update(uuid, existingPizza);
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