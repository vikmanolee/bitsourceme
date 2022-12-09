using Microsoft.Data.Sqlite;

namespace BitSourceMe.Data.Sqlite;

public static class DbSchema
{
    public const string SourceTableName = "Sources";
    public const string PriceTableName = "Prices";
    
    public static void Setup(SqliteConfiguration config)
    {
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = config.Filename,
            Mode = SqliteOpenMode.ReadWriteCreate,
        }.ToString();
        
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                $@"
                CREATE TABLE IF NOT EXISTS {PriceTableName} (
                    Id INTEGER NOT NULL PRIMARY KEY,
                    Price Text,
                    SourceCode Text,
                    FetchedDate Text
                );
                CREATE TABLE IF NOT EXISTS {SourceTableName} (
                    Id INTEGER NOT NULL PRIMARY KEY,
                    Code Text,
                    Title Text,
                    BaseUrl Text
                );
                INSERT INTO {SourceTableName} (Id, Code, Title) 
                VALUES
                    ( 1, 'bitfinex', 'BitFinex Com'),
                    ( 2, 'Bitstamp', 'BitStamp Net')
                ON CONFLICT(Id) DO NOTHING;
";
            command.ExecuteNonQuery();
        }
    }
}
