using BitSourceMe.Core.Abstractions.Models;

namespace BitSourceMe.Data;

public interface ISourceProvider
{
    Task<IEnumerable<TickerSource>> GetAll();
}
