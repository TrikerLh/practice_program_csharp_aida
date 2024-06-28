using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface QuotesService
{
    IList<Quote> GetQuotes(string word);
}