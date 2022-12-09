using BitSourceMe.Core.Abstractions.Models;
using Microsoft.Extensions.Options;

namespace BitSourceMe.Data.InMemory;

public class SourceProvider : ISourceProvider
{
    private readonly IEnumerable<TickerSource> _sources;

    public SourceProvider(IOptions<List<TickerSource>> sources)
    {
        _sources = sources.Value;
    }

    public Task<IEnumerable<TickerSource>> GetAll() => Task.FromResult(_sources);
}
