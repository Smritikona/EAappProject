using Microsoft.Data.Sqlite;
using ProductAPI.Data;

namespace EAFramework.Utilities;

public class DbHelperForSqlite
{
    public static SqliteCommand CreateDbConnection()
    {

        var connectionString = "Data Source=/Users/karthik/Downloads/EAApp_LocalMachine-6/ProductAPI/Product.db";

        var connection = new SqliteConnection(connectionString);
        connection.Open();

        return connection.CreateCommand();
    }


    public static Product GetProductByName(string productName)
    {
        var command = CreateDbConnection();

        command.CommandText = $"SELECT * FROM PRODUCTS Where Name = '{productName}'";

        using var reader = command.ExecuteReader();

        Product product = null;
        while (reader.Read())
            product = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                reader.GetInt32(4));

        return product;
    }
    
    
    public static Product GetProductById(string productId)
    {
        var command = CreateDbConnection();

        command.CommandText = $"SELECT * FROM PRODUCTS Where Id = '{productId}'";

        using var reader = command.ExecuteReader();

        Product product = null;
        while (reader.Read())
            product = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3),
                reader.GetInt32(4));

        return product;
    }

    public record Product(int? Id, string Name, string Description, int Price, int ProductType);
}