using Smartwyre.DeveloperTest.Types;
using Microsoft.Data.Sqlite;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore 
{
    private const string connectionString = "Data Source=:memory:";
    private SqliteConnection connection;
    private static RebateDataStore instance;

    private RebateDataStore()
    {
    }

    public static RebateDataStore Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RebateDataStore();
                instance.PerformDatabaseOperations();

            }
            return instance;
        }
    }

    private void PerformDatabaseOperations()
    {
        connection = new SqliteConnection(connectionString);
        connection.Open();

        // Create the in-memory database schema and perform database operations here using the connection
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                    CREATE TABLE Rebates (
                        Identifier TEXT PRIMARY KEY,
                        Incentive INTEGER,
                        Amount DECIMAL(18, 2),
                        Percentage DECIMAL(18, 2)
                    )";

            command.ExecuteNonQuery();
        }

        // Insert sample data
        InsertRebate(new Rebate
        {
            Identifier = "RB01",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 10.50m,
            Percentage = 10.5m
        });

        InsertRebate(new Rebate
        {
            Identifier = "RB02",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 20.75m,
            Percentage = 7.8m
        });

        InsertRebate(new Rebate
        {
            Identifier = "RB03",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 30.25m,
            Percentage = 4.2m
        });

        InsertRebate(new Rebate
        {
            Identifier = "RB04",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 0m,
            Percentage = 4.2m
        });

        InsertRebate(new Rebate
        {
            Identifier = "RB05",
            Incentive = IncentiveType.FixedRateRebate,
            Amount = 20.75m,
            Percentage = 0
        });

        InsertRebate(new Rebate
        {
            Identifier = "RB06",
            Incentive = IncentiveType.AmountPerUom,
            Amount = 0,
            Percentage = 0
        });
    }

    private void InsertRebate(Rebate incentive)
    {
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO Rebates (Identifier, Incentive, Amount, Percentage)
                VALUES (@Identifier, @Incentive, @Amount, @Percentage)";

            command.Parameters.AddWithValue("@Identifier", incentive.Identifier);
            command.Parameters.AddWithValue("@Incentive", (int)incentive.Incentive);
            command.Parameters.AddWithValue("@Amount", incentive.Amount);
            command.Parameters.AddWithValue("@Percentage", incentive.Percentage);

            command.ExecuteNonQuery();
        }
    }

    public Rebate GetRebate(string rebateIdentifier)
    {
        // Access database to retrieve account, code removed for brevity
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT Identifier, Incentive, Amount, Percentage FROM Rebates WHERE Identifier = @Identifier";
            command.Parameters.AddWithValue("@Identifier", rebateIdentifier);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var incentive = new Rebate
                    {
                        Identifier = reader.GetString(0),
                        Incentive = (IncentiveType)reader.GetInt32(1),
                        Amount = reader.GetDecimal(2),
                        Percentage = reader.GetDecimal(3)
                    };

                    return incentive;
                }
            }
        }

        return new Rebate();
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = @"
                UPDATE Rebates
                SET  Amount = @Amount
                WHERE Identifier = @Identifier";

            //command.Parameters.AddWithValue("@Incentive", (int)account.Incentive);
            command.Parameters.AddWithValue("@Amount", rebateAmount);
            //command.Parameters.AddWithValue("@Percentage", account.Percentage);
            command.Parameters.AddWithValue("@Identifier", account.Identifier);

            command.ExecuteNonQuery();
        }
        // Update account in database, code removed for brevity
    }
}
