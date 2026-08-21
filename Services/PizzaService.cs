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

    public async Task<Pizza?> GetById(int id) => await _db.Pizzas.FindAsync(id);
    public async Task<Pizza> Create(Pizza pizza)
    {
        _db.Pizzas.Add(pizza);
        await _db.SaveChangesAsync();
        return pizza;
    }

    public async Task<bool> Delete(int id)
    {
        var pizza = await _db.Pizzas.FindAsync(id);
        if (pizza is null)
            return false;

        _db.Pizzas.Remove(pizza);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(int id, Pizza pizza)
    {
        var existingPizza = await _db.Pizzas.FindAsync(id);
        if (existingPizza is null)
            return false;

        existingPizza.Name = pizza.Name;
        existingPizza.IsGlutenFree = pizza.IsGlutenFree;
        await _db.SaveChangesAsync();
        return true;
    }
}