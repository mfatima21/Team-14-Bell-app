namespace CreateSearchIndex;

public class Recipe
{
    public string Name { get; set; }
    
    public List<string> Steps { get; set; }
    
    public List<string> IngredientsWithQuantity { get; set; }
}