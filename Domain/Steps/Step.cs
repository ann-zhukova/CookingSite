using Domain.Recipes;

namespace Domain.Steps;

public class Step
{
    public Guid Id { get; set; }
    
    public int StepNumber { get; set; }
    
    public string StepDescription { get; set; }
    
    public Guid RecipetId { get; set; }
    
    public Recipe Recipe { get; set; }
}