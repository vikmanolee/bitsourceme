using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Core.Abstractions;

public interface IPriceService
{
    Task<TickerPrice> FetchNewPrice(string sourceCode);

    Task<IEnumerable<TickerPrice>> GetTickerPrices(SearchCriteria criteria);
}
