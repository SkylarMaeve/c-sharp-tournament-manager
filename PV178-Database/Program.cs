using Npgsql;

class Program
{
    static void Main(string[] args)
    {
        // Connection string to the PostgreSQL server (not a specific database)
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=None;";

        // SQL command to create a new database
        string createDbCommand = "CREATE DATABASE MyNewDatabase";

        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(createDbCommand, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("PV178-Database created successfully!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}