namespace BitSourceMe.Core.Abstractions;

public interface ITickerClientFactory
{
    ITickerClient GetClient(string sourceCode);
}
