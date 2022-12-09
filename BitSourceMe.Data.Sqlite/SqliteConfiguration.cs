namespace BitSourceMe.Data.Sqlite;

public class SqliteConfiguration
{
    public const string SectionName = "SqliteConfiguration";

    public string Filename { get; set; } = "random.db";
}
