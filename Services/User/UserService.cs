using MSilvaPizza.Models;
using MSilvaPizza.Services;

public class UserService : IUserService
{
    private readonly UserDb _db;
    public UserService(UserDb db)
    {
        _db = db;
    }
    public async Task<User> Create(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
    public async Task<User?> GetByUuid(Guid uuid) => await _db.Users.FindAsync(uuid);
    public async Task<bool> Update(Guid uuid, User user)
    {
        var existingUser = await _db.Users.FindAsync(uuid);
        if (existingUser is null)
            return false;

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        await _db.SaveChangesAsync();
        return true;
    }
}