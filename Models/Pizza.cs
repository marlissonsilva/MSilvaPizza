using System.ComponentModel.DataAnnotations;

namespace MSilvaPizza.Models;

public class Pizza
{
    [Key]
    public Guid Uuid { get; set; }
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
}