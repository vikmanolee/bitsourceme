using System.Text.Json;
using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using Microsoft.Extensions.Options;

namespace BitSourceMe.Core;

public abstract class GetJsonClientBase<TResponse> : ITickerClient
{
    protected readonly string BaseUrl;
    public abstract string ReferencingSourceCode { get; }
    protected abstract string TickerEndpoint { get; }
    protected abstract JsonSerializerOptions SerializeOptions { get; }
    protected abstract decimal GetTickerPrice(TResponse? response);

    protected GetJsonClientBase(IOptions<List<TickerSource>> sources)
    {
        BaseUrl = sources.Value.Single(s => s.Code.Equals(ReferencingSourceCode)).BaseUrl;
    }

    public async Task<decimal> GetNewPrice()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(BaseUrl + TickerEndpoint),
            Headers =
            {
                { "accept", "application/json" }
            }
        };
        
        using var response = await client.SendAsync(request);
        
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var ticker = JsonSerializer.Deserialize<TResponse>(body, SerializeOptions);
        return GetTickerPrice(ticker);
    }
}
