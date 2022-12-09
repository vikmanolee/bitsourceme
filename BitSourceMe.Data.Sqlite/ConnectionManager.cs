using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace BitSourceMe.Data.Sqlite;

public class ConnectionManager
{
    private readonly string _connectionString;

    public ConnectionManager(IOptions<SqliteConfiguration> config)
    {
        _connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = config.Value.Filename,
            Mode = SqliteOpenMode.ReadWrite,
        }.ToString();
    }

    public SqliteConnection GetConnection() => new SqliteConnection(_connectionString);
}
