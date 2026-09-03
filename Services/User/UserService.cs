using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSilvaPizza.Models;
using MSilvaPizza.Services;

public class UserService : IUserService
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly UserDb _db;
    public UserService(UserDb db)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<User>();
    }
    public async Task<User> Create(User user, string plainTextPassword)
    {
        user.Password = _passwordHasher.HashPassword(user, plainTextPassword);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetByUuid(Guid uuid) => await _db.Users.FindAsync(uuid);
    public async Task<User?> GetByUsername(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.Username == username);
    }
    public async Task<bool> UsernameExists(string username)
    {
        return await _db.Users.AnyAsync(user => user.Username == username);
    }
    public async Task<bool> Update(Guid uuid, User user, string? newPlainTextPassword)
    {
        var existingUser = await _db.Users.FindAsync(uuid);
        if (existingUser is null)
            return false;

        existingUser.Username = user.Username;

        if (!string.IsNullOrWhiteSpace(newPlainTextPassword))
        {
            existingUser.Password = _passwordHasher.HashPassword(existingUser, newPlainTextPassword);
        }
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Login(User user, string hashFromDatabase, string plainTextPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashFromDatabase, plainTextPassword);
        return result == PasswordVerificationResult.Success;
    }
}