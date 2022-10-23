using Lucene.Net.Search.Similarities;

namespace CreateSearchIndex;

public class IgnoreInverseDocumentCount : DefaultSimilarity
{
    public override float Idf(long docFreq, long numDocs)
    {
        // Very common and very rare terms are not 
        // penalized by the search engine.
        return 1;
    }
}