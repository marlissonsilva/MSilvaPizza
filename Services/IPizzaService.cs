using MSilvaPizza.Models;

namespace MSilvaPizza.Services;

public interface IPizzaService
{
    Task<List<Pizza>> GetAll();
    Task<Pizza?> GetById(Guid uuid);
    Task<Pizza> Create(Pizza pizza);
    Task<bool> Update(Guid uuid, Pizza updatedPizza);
    Task<bool> Delete(Guid uuid);
}