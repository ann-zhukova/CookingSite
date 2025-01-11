using System.Text.Json.Serialization;

namespace Domain;

public class RecipeFilter
{
    public int? MinTime { get; set; }
    
    public int? MaxTime { get; set; }
    
    public List<string>? Types { get; set; }
    
    public List<string>? Ingredients { get; set; }
    
    // Параметры пагинации
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    
    public string SortBy { get; set; }
}