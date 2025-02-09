using System.Text.Json.Serialization;

namespace Domain;

public class RecipeFilter
{
    public int? MinTime { get; set; }
    
    public int? MaxTime { get; set; }
    
    public List<Guid>? Types { get; set; }
    
    public List<Guid>? Ingredients { get; set; }
    
    // Параметры пагинации
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 6;
    
    public string SortBy { get; set; }
}