using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using BitSourceMe.Data;

namespace BitSourceMe.Core.Services;

public class PriceService : IPriceService
{
    private readonly IPriceProvider _priceProvider;
    private readonly ITickerClientFactory _clientFactory;

    public PriceService(IPriceProvider priceProvider, ITickerClientFactory clientFactory)
    {
        _priceProvider = priceProvider;
        _clientFactory = clientFactory;
    }

    public async Task<TickerPrice> FetchNewPrice(string sourceCode)
    {
        var client = _clientFactory.GetClient(sourceCode);
        var price = await client.GetNewPrice();

        var tickerPrice = new TickerPrice
        {
            Price = price,
            FetchedDate = DateTime.Now,
            SourceCode = sourceCode
        };
        
        await _priceProvider.Store(tickerPrice);
        
        return tickerPrice;
    }

    public async Task<IEnumerable<TickerPrice>> GetTickerPrices(SearchCriteria criteria)
    {
        return await _priceProvider.GetWithCriteria(criteria);
    }
}
