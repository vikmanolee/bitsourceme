using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using BitSourceMe.Data;

namespace BitSourceMe.Core.Services;

public class SourceService : ISourceService
{
    private readonly ISourceProvider _sourceProvider;

    public SourceService(ISourceProvider sourceProvider)
    {
        _sourceProvider = sourceProvider;
    }

    public async Task<IEnumerable<TickerSource>> GetAllSources()
    {
        return await _sourceProvider.GetAll();
    }
}
