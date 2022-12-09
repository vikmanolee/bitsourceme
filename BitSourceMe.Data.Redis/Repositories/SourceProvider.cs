using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Data.Redis.Repositories;

public class SourceProvider : ISourceProvider
{
    public Task<IEnumerable<TickerSource>> GetAll()
    {
        throw new NotImplementedException();
    }
}
