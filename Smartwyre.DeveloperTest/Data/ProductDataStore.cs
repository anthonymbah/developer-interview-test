using Microsoft.Data.Sqlite;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore
{
    private static ProductDataStore instance;
    private static SqliteConnection connection;
    private const string connectionString = "Data Source=:memory:";

    private ProductDataStore()
    {
        connection = new SqliteConnection("Data Source=:memory:");
    }

    public static ProductDataStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProductDataStore();
                instance.PerformDatabaseOperations();
            }

            return instance;
        }
    }

    private void PerformDatabaseOperations()
    {
        connection = new SqliteConnection(connectionString);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS Products (Id INTEGER PRIMARY KEY AUTOINCREMENT, Identifier TEXT, Price DECIMAL, Uom TEXT, SupportedIncentives INTEGER)";
            command.ExecuteNonQuery();
        }

        InsertProduct(new Product
        {
            Identifier = "PDT01",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
            Price = 10.99m,
            Uom = "rate"
        });

        InsertProduct(new Product
        {
            Identifier = "PDT02",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 19.99m,
            Uom = "each"
        });
        InsertProduct(new Product
        {
            Identifier = "PDT03",
            SupportedIncentives = SupportedIncentiveType.AmountPerUom,
            Price = 5.99m,
            Uom = "cash"
        });

        InsertProduct(new Product
        {
            Identifier = "PDT04",
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 0,
            Uom = "cash"
        });
    }

    public void InsertProduct(Product product)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO Products (Identifier, Price, Uom, SupportedIncentives) VALUES (@Identifier, @Price, @Uom, @SupportedIncentives)";
            command.Parameters.AddWithValue("@Identifier", product.Identifier);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Uom", product.Uom);
            command.Parameters.AddWithValue("@SupportedIncentives", (int)product.SupportedIncentives);

            command.ExecuteNonQuery();
        }

    }

    public Product GetProduct(string productIdentifier)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Id, Identifier, Price, Uom, SupportedIncentives FROM Products WHERE Identifier = @Identifier";
            command.Parameters.AddWithValue("@Identifier", productIdentifier);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Product product = new Product
                    {
                        Id = reader.GetInt32(0),
                        Identifier = reader.GetString(1),
                        Price = reader.GetDecimal(2),
                        Uom = reader.GetString(3),
                        SupportedIncentives = (SupportedIncentiveType)reader.GetInt32(4)
                    };

                    return product;
                }
            }
        }

        return new Product();
    }

    public void UpdateProduct(Product product)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Products SET Identifier = @Identifier, Price = @Price, Uom = @Uom, SupportedIncentives = @SupportedIncentives WHERE Id = @Id";
            command.Parameters.AddWithValue("@Identifier", product.Identifier);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.Parameters.AddWithValue("@Uom", product.Uom);
            command.Parameters.AddWithValue("@SupportedIncentives", (int)product.SupportedIncentives);
            command.Parameters.AddWithValue("@Id", product.Id);

            command.ExecuteNonQuery();
        }
    }
}
