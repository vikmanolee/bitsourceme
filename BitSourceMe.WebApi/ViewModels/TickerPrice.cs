namespace BitSourceMe.WebApi.ViewModels;

public class TickerPrice
{
    public string Price { get; set; } = null!;

    public DateTime FetchedDate { get; set; }

    public string SourceCode { get; set; } = null!;
}
