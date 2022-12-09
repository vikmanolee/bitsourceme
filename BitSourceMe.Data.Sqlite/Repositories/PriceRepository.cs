using BitSourceMe.Core.Abstractions.Models;
using Dapper;

namespace BitSourceMe.Data.Sqlite.Repositories;

public class PriceRepository : SqliteRepositoryBase, IPriceProvider
{
    private const int MaxLookBehindMonths = 6;

    public PriceRepository(ConnectionManager manager) : base(manager)
    {
    }

    public async Task Store(TickerPrice tickerPrice)
    {
        int newId = await Connection.QuerySingleAsync<int>(
            $"INSERT INTO {DbSchema.PriceTableName} (Price, FetchedDate, SourceCode) VALUES (@price, @fetchedDate, @sourceCode);" + 
                "SELECT last_insert_rowid()", new 
            {
                price = tickerPrice.Price,
                fetchedDate = tickerPrice.FetchedDate,
                sourceCode = tickerPrice.SourceCode
            });

        tickerPrice.Id = newId;
    }

    public async Task<IEnumerable<TickerPrice>> GetWithCriteria(SearchCriteria criteria)
    {
        var maxQuerableDate = DateTime.Now.AddMonths(MaxLookBehindMonths);
        var dateFrom = criteria.DateRange.DateFrom <= maxQuerableDate
            ? criteria.DateRange.DateFrom
            : maxQuerableDate;

        var builder = new SqlBuilder()
            .Where("FetchedDate >= @dateFrom", new { dateFrom });

        if (!string.IsNullOrEmpty(criteria.SourceCode))
            builder.Where("SourceCode = @sourceCode", new { sourceCode = criteria.SourceCode });

        if (criteria.DateRange.DateTo.HasValue)
            builder.Where("FetchedDate <= @dateTo", new { dateTo = criteria.DateRange.DateTo });

        var query = builder.AddTemplate(
            $"SELECT [Id], [Price], [FetchedDate], [SourceCode] FROM {DbSchema.PriceTableName} /**where**/");
        return await Connection.QueryAsync<TickerPrice>(query.RawSql, query.Parameters);
    }
}
