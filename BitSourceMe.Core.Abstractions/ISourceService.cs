using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Core.Abstractions;

public interface ISourceService
{
    Task<IEnumerable<TickerSource>> GetAllSources();
}
