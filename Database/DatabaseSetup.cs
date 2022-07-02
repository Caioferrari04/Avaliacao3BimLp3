using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Database;

public class DatabaseSetup 
{
    private readonly DatabaseConfig _databaseConfig;
    public DatabaseSetup(DatabaseConfig databaseConfig) 
    {
        _databaseConfig = databaseConfig;
        CreateProductTable();
    }

    private void CreateProductTable() 
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Product(
                Id int not null primary key,
                Name varchar(100) not null,
                Price double not null,
                Active boolean not null
            );
        ");
    }   
}
