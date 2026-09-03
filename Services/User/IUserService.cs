using MSilvaPizza.Models;

namespace MSilvaPizza.Services;

public interface IUserService
{
    Task<User> Create(User user, string plainTextPassword);
    Task<User?> GetByUuid(Guid uuid);
    Task<User?> GetByUsername(string username);
    Task<bool> UsernameExists(string username);
    Task<bool> Update(Guid uuid, User updatedUser, string? newPlainTextPassword);
    Task<bool> Login(User user, string plainTextPassword, string hashFromDatabase);
}