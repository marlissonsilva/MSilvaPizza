using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MSilvaPizza.Models;

[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    public Guid Uuid { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}


public class UserDb : DbContext
{
    public UserDb(DbContextOptions<UserDb> options) : base(options) { }
    public DbSet<User> Users { get; set; } = null!;
}
