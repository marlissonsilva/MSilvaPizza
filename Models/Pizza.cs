using System.ComponentModel.DataAnnotations;

namespace MSilvaPizza.Models;

public class Pizza
{
    [Key]
    public Guid Uuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsGlutenFree { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string DoughType { get; set; } = string.Empty;
    public List<string> Ingredients { get; set; } = new();
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}