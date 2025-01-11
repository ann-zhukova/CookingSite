namespace Front.Models.Recipe;

public class RecipesResponseJsModel: BaseResponseJsModel
{
    public ICollection<RecipeResponseJs> Recipes { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 10;
    
    public int TotalItems { get; set; }
}