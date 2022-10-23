namespace CreateSearchIndex;

public class BellRecommendations
{
    private readonly Dictionary<string, List<BellProduct>> _ingredientToBellProduct = new();

    public BellRecommendations()
    {
        var sausageReplacements = new List<BellProduct>
        {
            new()
            {
                Name = "ST.GALLER SCHÜBLIG",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/st.galler-schueblig-105761/"
            },
            new()
            {
                Name = "FRANKFURTERLI",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/frankfurterli-158641/"
            },
            new()
            {
                Name = "ST. GALLER OLMA BRATWURST",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/st.-galler-olma-bratwurst--178749/"
            },
        };
        
        _ingredientToBellProduct["sausage"] = sausageReplacements;
        _ingredientToBellProduct["bratwust"] = sausageReplacements;

        _ingredientToBellProduct["chicken"] = new List<BellProduct>()
        {
            new()
            {
                Name = "POULET WIENERLI",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-wienerli-116142/"
            },
            new()
            {
                Name = "POULET BRATWURST",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-bratwurst-116143/"
            },
            new()
            {
                Name = "POULET CERVECAS",
                Url = "https://www.bell.ch/de/produkte/alle-produkte/produkt/poulet-cervelas-116141/"
            },
        };

    }

    public List<BellProduct> GetRecommendations(string ingredients)
    {
        List<BellProduct> recommendations = new List<BellProduct>();

        foreach (var searchTerm in ingredients.Split(' '))
        {
            if (!_ingredientToBellProduct.TryGetValue(searchTerm, out List<BellProduct> products))
            {
                continue;
            }

            BellProduct product = products[Random.Shared.Next(products.Count)];
            if (!recommendations.Contains(product))
            {
                recommendations.Add(product);   
            }
        }
        
        return recommendations;
    }

}