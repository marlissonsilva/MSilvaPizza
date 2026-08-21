using MSilvaPizza.Models;

namespace MSilvaPizza.Services;

public interface IPizzaService
{
    Task<List<Pizza>> GetAll();
    Task<Pizza?> GetById(int id);
    Task<Pizza> Create(Pizza pizza);
    Task<bool> Update(int id, Pizza updatedPizza);
    Task<bool> Delete(int id);
}