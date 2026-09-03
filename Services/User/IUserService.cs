using MSilvaPizza.Models;

namespace MSilvaPizza.Services;

public interface IUserService
{
    Task<User> Create(User user);
    Task<User?> GetByUuid(Guid uuid);
    Task<bool> Update(Guid uuid, User updatedUser);
}