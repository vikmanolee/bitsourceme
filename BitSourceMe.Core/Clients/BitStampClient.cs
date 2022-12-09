using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using Microsoft.Extensions.Options;

#pragma warning disable CS8618

namespace BitSourceMe.Core.Clients;

public class BitStampClient : GetJsonClientBase<BitStampResponse>, ITickerClient
{
    public override string ReferencingSourceCode => "bitstamp";
    protected override string TickerEndpoint => "/api/v2/ticker/btcusd/";
    protected override JsonSerializerOptions SerializeOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true
    };
    protected override decimal GetTickerPrice(BitStampResponse? response) 
        => decimal.Parse(response?.VolumeWeightedAveragePrice ?? "0", NumberStyles.Currency);

    public BitStampClient(IOptions<List<TickerSource>> sources) : base(sources)
    {
    }
}

public class BitStampResponse
{
    public string Last { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    [JsonPropertyName("vwap")]
    public string VolumeWeightedAveragePrice { get; set; }
    public string Volume { get; set; }
    public string Bid { get; set; }
    public string Ask { get; set; }
    public string Timestamp { get; set; }
    [JsonPropertyName("open_24")]
    public string Open24 { get; set; }
    [JsonPropertyName("percent_change_24")]
    public string Open24Perc { get; set; }
}
