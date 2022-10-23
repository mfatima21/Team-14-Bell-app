using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using CreateSearchIndex;
using CsvHelper;
using CsvHelper.Configuration;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Directory = Lucene.Net.Store.Directory;

public class Program
{
    // Ensures index backward compatibility
    const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
    const string FIELD_INGREDIENTS = "INGREDIENTS";
    const string FIELD_RECIPE_NAME = "RECIPE";
    const string FIELD_INGREDIENTS_RAW = "INGREDIENTS_RAW";
    const string FIELD_STEPS = "STEPS";
    public static BellRecommendations BellRecommendations = new();
    
    public static int Main(string[] args)
    {
        string? basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string indexPath = Path.Combine(basePath, "index");
        
        // DeleteIndex(indexPath);
        using FSDirectory? dir = FSDirectory.Open(indexPath);
        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        
        // CreateIndex(dir, analyzer);
        // return 0;
        
        string? searchTerms = string.Join(" ", args);
        // string? searchTerms = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(searchTerms))
        {
            // No search terms -> No output
            return -1;
        }
        
        if (TryGetRecipes(dir, analyzer, searchTerms,  out List<Recipe> recipes))
        {
            string jsonString = JsonSerializer.Serialize(recipes, new JsonSerializerOptions
            {
                // Keep unicode symbols intact
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });
            Console.Out.WriteLine(jsonString);
            return 0;
        }

        // No results -> No output
        return -1;
    }

    public static void DeleteIndex(string indexPath)
    {
        if (System.IO.Directory.Exists(indexPath))
        {
            System.IO.Directory.Delete(indexPath, true);
        }
    }
    
    public static void CreateIndex(Directory dir, StandardAnalyzer analyzer)
    {
        Trace.WriteLine("Creating index");
        Stopwatch watch = Stopwatch.StartNew();

        var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer)
        {
            Similarity = new IgnoreInverseDocumentCount()
        };
        using var writer = new IndexWriter(dir, indexConfig);
        using var reader = new StreamReader(@"/Users/rb/Downloads/recipes_w_search_terms.csv");
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Escape = '"',
        });
        
        int recipeCount = 0;
        int? MAX_RECIPE_COUNT = null;
        IEnumerable<Row> records = csvReader.GetRecords<Row>();
        Regex matchEntries = new Regex(@"'(.*?)'", RegexOptions.Compiled);

        foreach (Row record in records)
        {
            if (MAX_RECIPE_COUNT is { } maxRecipeCount && recipeCount > maxRecipeCount)
            {
                // We are done.
                break;
            }

            List<string> ingredients = new List<string>();
            foreach (Match regexMatch in matchEntries.Matches(record.Ingredients))
            {
                ingredients.Add(regexMatch.Groups[1].Value);
            }
        
            string ingredientsAsString = string.Join(' ', ingredients);

            Document doc = new Document
            {
                // This field is searchable by the user.
                new TextField(FIELD_INGREDIENTS, ingredientsAsString, Field.Store.YES),
                
                // These values can be returned from the found document. (and returned to the client)
                new StoredField(FIELD_RECIPE_NAME, record.Name),
                new StoredField(FIELD_INGREDIENTS_RAW, record.IngredientsRaw),
                new StoredField(FIELD_STEPS, record.Steps)
            };
            writer.AddDocument(doc);
            recipeCount++;
        }

        writer.Flush(triggerMerge: false, applyAllDeletes: false);
        watch.Stop();
        Trace.WriteLine($"Done ({watch.ElapsedMilliseconds}). Index created with {writer.NumDocs} documents.");
    }

    public static bool TryGetRecipes(Directory dir, StandardAnalyzer analyzer, string searchTerms, out List<Recipe> recipes)
    {
        recipes = new List<Recipe>();

        DirectoryReader directoryReader = DirectoryReader.Open(dir);
        IndexSearcher indexSearcher = new IndexSearcher(directoryReader);
       
        QueryParser parser = new QueryParser(AppLuceneVersion, FIELD_INGREDIENTS, analyzer);
        
        Query query = parser.Parse(searchTerms);
        ScoreDoc[] hits = indexSearcher.Search(query, null, 10).ScoreDocs;

        Trace.WriteLine("Hits:");

        // Every Bell product should only be recommended once per query, 
        // so that the user is not overloaded with Bell products.
        // Every recipe checks if the recommendation is already used. If it is,
        // discard it.
        // --> Cool side effect: Keep recipes at the top.
        List<BellProduct> allRecipesRecommendations = new List<BellProduct>();

        foreach (ScoreDoc hit in hits)
        {
            Document? foundDoc = indexSearcher.Doc(hit.Doc);
            recipes.Add(ConvertDocumentToRecipe(foundDoc, allRecipesRecommendations));   
        }

        Recipe ConvertDocumentToRecipe(Document doc, List<BellProduct> alreadyRecommended)
        {
            Regex matchSteps = new Regex(@"'(.*?)(?<!\\)'", RegexOptions.Compiled);
            Regex matchIngredientsRaw = new Regex("\"(.*?)(?<!\\\\)\"", RegexOptions.Compiled);

            string stepsField = doc.Get(FIELD_STEPS);
            List<string> steps = new List<string>();
            foreach (Match match in matchSteps.Matches(stepsField))
            {
                steps.Add(match.Groups[1].Value);
            }

            List<string> ingredientsRaw = new List<string>();
            string ingredientsRawField = doc.Get(FIELD_INGREDIENTS_RAW);
            foreach (Match match in matchIngredientsRaw.Matches(ingredientsRawField))
            {
                string value = match.Groups[1].Value;
                
                // Replace multiple whitespace characters with single whitespace.
                value = Regex.Replace(value, @"\s+", " ");
                
                ingredientsRaw.Add(value);
            }
            
            string ingredients = doc.Get(FIELD_INGREDIENTS);
            List<BellProduct> recommendations = BellRecommendations.GetRecommendations(ingredients);
            List<BellProduct> filteredRecommendations = new List<BellProduct>();

            foreach (BellProduct recommendedProduct in recommendations)
            {
                if (alreadyRecommended.Contains(recommendedProduct))
                {
                    // Already recommended for another recipe. Do nothing.
                    continue;
                }
                
                // Add the recommendation to the current product
                filteredRecommendations.Add(recommendedProduct);
                
                // Add the recommendation to the already recommended products
                // so it is not used again.
                alreadyRecommended.Add(recommendedProduct);
            }
            
            return new Recipe
            {
                Name = doc.Get(FIELD_RECIPE_NAME),
                IngredientsWithQuantity = ingredientsRaw,
                Steps = steps,
                Recommendations = filteredRecommendations,
            };
        }

        return recipes.Count > 0;
    }
}