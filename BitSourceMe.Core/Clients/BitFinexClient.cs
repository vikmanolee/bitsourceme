using System.Text.Json;
using System.Text.Json.Serialization;
using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using Microsoft.Extensions.Options;
#pragma warning disable CS8618

namespace BitSourceMe.Core.Clients;

public class BitFinexClient : GetJsonClientBase<BitFinexResponse>, ITickerClient
{
    public override string ReferencingSourceCode => "bitfinex";
    protected override string TickerEndpoint => "/v1/pubticker/BTCUSD";
    protected override JsonSerializerOptions SerializeOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected override decimal GetTickerPrice(BitFinexResponse? response) 
        => decimal.Parse(response?.Mid ?? "0");

    public BitFinexClient(IOptions<List<TickerSource>> sources) : base(sources)
    {
    }
}

public class BitFinexResponse
{
    public string Mid { get; set; }
    public string Bid { get; set; }
    public string Ask { get; set; }
    [JsonPropertyName("last_price")]
    public string LastPrice { get; set; }
    public string Low { get; set; }
    public string High { get; set; }
    public string Volume { get; set; }
    public string Timestamp { get; set; }
}
