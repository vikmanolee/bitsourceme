using Microsoft.Data.Sqlite;

namespace BitSourceMe.Data.Sqlite;

public abstract class SqliteRepositoryBase : IDisposable
{
    protected SqliteConnection Connection;
    
    protected SqliteRepositoryBase(ConnectionManager manager)
    {
        Connection = manager.GetConnection();
        Connection.Open();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }
}
