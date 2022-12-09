using BitSourceMe.Core.Abstractions.Models;
using Dapper;

namespace BitSourceMe.Data.Sqlite.Repositories;

public class SourceRepository : SqliteRepositoryBase, ISourceProvider
{
    public SourceRepository(ConnectionManager manager) : base(manager)
    {
    }

    public async Task<IEnumerable<TickerSource>> GetAll()
    {
        return await Connection.QueryAsync<TickerSource>(
            $"SELECT * FROM {DbSchema.SourceTableName}");
    }
}
