using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Models;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repository;

public class ProductRepository
{
    private readonly DatabaseConfig _databaseConfig;
    public ProductRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Product VALUES (@Id, @Name, @Price, @Active);", product);
        return product;
    }
    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Product WHERE Id=@Id", new { @Id = id });
    }
    public void Enable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Product SET Active=true WHERE Id=@Id", new { @Id = id });
    }

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Product SET Active=false WHERE Id=@Id", new { @Id = id });
    }

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.Query<Product>("SELECT * FROM Product").ToList();
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.ExecuteScalar<bool>("SELECT COUNT(ID) FROM Product WHERE Id=@Id", new { @Id = id });
    }

    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.Query<Product>("SELECT * FROM Product WHERE Price BETWEEN @InitialPrice AND @EndPrice", new
        {
            @InitialPrice = initialPrice,
            @EndPrice = endPrice
        }).ToList();
    }
    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.Query<Product>("SELECT * FROM Product WHERE Price > @Price", new
        {
            @Price = price,
        }).ToList();
    }
    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.Query<Product>("SELECT * FROM Product WHERE Price < @Price", new
        {
            @Price = price,
        }).ToList();
    }
    public double GetAveragePrice()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        return connection.ExecuteScalar<double>("SELECT AVG(Price) FROM Product");
    }
}
