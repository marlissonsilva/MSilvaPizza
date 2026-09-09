public class UpdatePizzaDto
{
    public string? Name { get; set; }
    public bool? IsGlutenFree { get; set; }
    public string? Description { get; set; }
    public string? Width { get; set; }
    public string? DoughType { get; set; }
    public List<string>? Ingredients { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
}