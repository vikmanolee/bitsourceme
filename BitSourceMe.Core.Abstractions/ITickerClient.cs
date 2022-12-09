namespace BitSourceMe.Core.Abstractions;

public interface ITickerClient
{
    public string ReferencingSourceCode { get; }

    Task<decimal> GetNewPrice();
}
