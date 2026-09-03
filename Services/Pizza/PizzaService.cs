using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MSilvaPizza.Models;
namespace MSilvaPizza.Services;

public class PizzaService : IPizzaService

{
    private readonly PizzaDb _db;

    public PizzaService(PizzaDb db)
    {
        _db = db;
    }
    public async Task<List<Pizza>> GetAll() => await _db.Pizzas.ToListAsync();

    public async Task<Pizza?> GetById(Guid uuid) => await _db.Pizzas.FindAsync(uuid);
    public async Task<Pizza> Create(Pizza pizza)
    {
        _db.Pizzas.Add(pizza);
        await _db.SaveChangesAsync();
        return pizza;
    }

    public async Task<bool> Delete(Guid uuid)
    {
        var pizza = await _db.Pizzas.FindAsync(uuid);
        if (pizza is null)
            return false;

        _db.Pizzas.Remove(pizza);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Guid uuid, Pizza pizza)
    {
        var existingPizza = await _db.Pizzas.FindAsync(uuid);
        if (existingPizza is null)
            return false;

        existingPizza.Name = pizza.Name;
        existingPizza.IsGlutenFree = pizza.IsGlutenFree;
        existingPizza.Description = pizza.Description;
        existingPizza.DoughType = pizza.DoughType;
        existingPizza.Width = pizza.Width;
        existingPizza.ImageUrl = pizza.ImageUrl;
        existingPizza.Price = pizza.Price;
        existingPizza.Ingredients = pizza.Ingredients;
        await _db.SaveChangesAsync();
        return true;
    }
}