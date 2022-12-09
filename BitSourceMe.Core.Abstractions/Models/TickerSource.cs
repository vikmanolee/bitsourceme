namespace BitSourceMe.Core.Abstractions.Models;

public class TickerSource
{
    public string Code { get; set; } = null!;
    
    public string Title { get; set; } = null!;

    public string BaseUrl { get; set; } = null!;
}
