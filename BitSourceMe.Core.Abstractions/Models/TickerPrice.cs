namespace BitSourceMe.Core.Abstractions.Models;

public class TickerPrice
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public DateTime FetchedDate { get; set; }

    public string SourceCode { get; set; } = null!;
}
