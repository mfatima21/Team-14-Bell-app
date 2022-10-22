using CsvHelper.Configuration.Attributes;

namespace CreateSearchIndex;

public class Row
{
    [Name("id")]
    public int Id { get; set; }
    
    [Name("name")]
    public string Name { get; set; }
    
    [Name("description")]
    public string Description { get; set; }
    
    [Name("ingredients")]
    public string Ingredients { get; set; }
    
    [Name("ingredients_raw_str")]
    public string IngredientsRaw { get; set; }
    
    [Name("serving_size")]
    public string ServingSize { get; set; }    
    
    [Name("servings")]
    public int ServingsCount { get; set; }
    
    [Name("steps")]
    public string Steps { get; set; }
    
    [Name("tags")]
    public string Tags { get; set; }
    
    [Name("search_terms")]
    public string SearchTerms { get; set; }
}