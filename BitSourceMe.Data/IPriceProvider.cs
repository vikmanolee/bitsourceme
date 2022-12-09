using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Data;

public interface IPriceProvider
{
    Task Store(TickerPrice tickerPrice);

    Task<IEnumerable<TickerPrice>> GetWithCriteria(SearchCriteria criteria);
}
