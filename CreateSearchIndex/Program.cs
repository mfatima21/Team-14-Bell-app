using System.Diagnostics;
using System.Globalization;
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
    
    public static void Main(string[] args)
    {
        // Construct a machine-independent path for the index   
        string? basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        string indexPath = Path.Combine(basePath, "index");
        
        // DeleteIndex(indexPath);
        using FSDirectory? dir = FSDirectory.Open(indexPath);
        StandardAnalyzer analyzer = new StandardAnalyzer(AppLuceneVersion);
        
        // CreateIndex(dir, analyzer);
        SearchInIndex(dir, analyzer);
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
        Console.WriteLine("Creating index");
        Stopwatch watch = Stopwatch.StartNew();
        
        // Create an analyzer to process the text

        // Create an index writer
        var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
        using var writer = new IndexWriter(dir, indexConfig);
        
        using var reader = new StreamReader(@"/Users/rb/Downloads/recipes_w_search_terms.csv");
        using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            Escape = '"',
        });
        
        int recipeCount = 0;
        int? MAX_RECIPE_COUNT = null;
        // MAX_RECIPE_COUNT = 10000;
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
                new StoredField(FIELD_RECIPE_NAME, record.Name),
                new TextField(FIELD_INGREDIENTS, ingredientsAsString, Field.Store.NO)
            };
            writer.AddDocument(doc);
            recipeCount++;
        }

        writer.Flush(triggerMerge: false, applyAllDeletes: false);
        watch.Stop();
        Console.WriteLine($"Done ({watch.ElapsedMilliseconds}). Index created with {writer.NumDocs} documents.");
    }

    public static void SearchInIndex(Directory dir, StandardAnalyzer analyzer)
    {
        Console.Write("Enter search terms: ");
        string? searchTerms = Console.ReadLine();

        if (searchTerms is null)
        {
            return;
        }
        
        DirectoryReader ireader = DirectoryReader.Open(dir);
        IndexSearcher isearcher = new IndexSearcher(ireader);
        
        // Parse a simple query that searches for "text":
        QueryParser parser = new QueryParser(AppLuceneVersion, FIELD_INGREDIENTS, analyzer);
        
        Query query = parser.Parse(searchTerms);
        ScoreDoc[] hits = isearcher.Search(query, null, 10).ScoreDocs;

        Console.Write("Hits:");
        foreach (ScoreDoc hit in hits)
        {
            Document? foundDoc = isearcher.Doc(hit.Doc);
            Console.WriteLine(foundDoc.Get(FIELD_RECIPE_NAME));
        }
    }
}