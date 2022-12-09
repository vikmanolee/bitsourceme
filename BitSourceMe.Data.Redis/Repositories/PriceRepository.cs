using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Data.Redis.Repositories;

public class PriceRepository : IPriceProvider
{
    public Task Store(TickerPrice tickerPrice)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TickerPrice>> GetWithCriteria(SearchCriteria criteria)
    {
        throw new NotImplementedException();
    }
}
