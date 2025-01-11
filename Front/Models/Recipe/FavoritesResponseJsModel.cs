namespace Front.Models.Recipe;

public class FavoritesResponseJsModel: BaseResponseJsModel
{
    public ICollection<RecipeResponseJs> Recipes { get; set; }
}