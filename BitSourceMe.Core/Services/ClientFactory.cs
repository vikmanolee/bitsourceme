using System.Collections.Immutable;
using BitSourceMe.Core.Abstractions;

namespace BitSourceMe.Core.Services;

public class ClientFactory : ITickerClientFactory
{
    private readonly ImmutableDictionary<string, ITickerClient> _clients;

    public ClientFactory(IEnumerable<ITickerClient> clients)
    {
        _clients = clients.ToImmutableDictionary(c => c.ReferencingSourceCode);
    }

    public ITickerClient GetClient(string sourceCode)
    {
        if (_clients.TryGetValue(sourceCode, out var client))
            return client;

        throw new ArgumentException("No client for this source", nameof(sourceCode));
    }
}
