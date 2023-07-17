using Microsoft.Data.Sqlite;
using Smartwyre.DeveloperTest.Types;


namespace Smartwyre.DeveloperTest.Services
{
    class DbService
    {
        private const string ConnectionString = "Data Source=:memory:";
        private SqliteConnection connection;
        private static DbService instance;

        private DbService()
        {
            // Private constructor to prevent direct instantiation
        }

        public static DbService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbService();
                    instance.PerformDatabaseOperations();

                }
                return instance;
            }
        }

        private void PerformDatabaseOperations()
        {
            connection = new SqliteConnection(ConnectionString);
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
                Identifier = "ABC123",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 1000.50m,
                Percentage = 10.5m
            });

            InsertRebate(new Rebate
            {
                Identifier = "DEF456",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 2000.75m,
                Percentage = 7.8m
            });

            InsertRebate(new Rebate
            {
                Identifier = "JHI789",
                Incentive = IncentiveType.AmountPerUom,
                Amount = 3000.25m,
                Percentage = 11.9m
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

            return null;
        }

        public void UpdateRebate(Rebate account, decimal rebateAmount)
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                UPDATE Rebates
                SET Incentive = @Incentive Amount = @Amount, Percentage = @Percentage
                WHERE Identifier = @Identifier";

                command.Parameters.AddWithValue("@Incentive", (int)account.Incentive);
                command.Parameters.AddWithValue("@Amount", rebateAmount);
                command.Parameters.AddWithValue("@Percentage", account.Percentage);
                command.Parameters.AddWithValue("@Identifier", account.Identifier);

                command.ExecuteNonQuery();
            }
        }
    }
}
