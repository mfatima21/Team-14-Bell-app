namespace CreateSearchIndex;

public class Recipe
{
    public string Name { get; set; }

    public List<string> Steps { get; set; } = new();

    public List<string> IngredientsWithQuantity { get; set; } = new();

    public List<BellProduct> Recommendations { get; set; } = new();
}