using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Data;

namespace API.Core;
public class DB : IDisposable
{
    private readonly SqliteConnection cnn = new(Constants.DB._CONNECTION_STRING_);
    public SqliteCommand command { get; private set; } = new();
    public void NewCommand(string query) => command = new(query, cnn);
    public void Parameter(string parameter, dynamic value) => command.Parameters.AddWithValue(parameter, value);
    public void Command(string sql) => command = new(sql, cnn);
    public void Connect()
    {
        try
        {
            Batteries.Init();
            if (cnn != null || cnn?.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine($"Error connecting to the database: {ex.Message}");
            throw;
        }
    }
    public void Disconnect()
    {
        if (cnn != null && cnn.State != ConnectionState.Closed)
        {
            cnn.Close();
            command.Dispose();
        }
    }
    public dynamic Execute()
    {
        if (cnn?.State == ConnectionState.Closed)
        {
            Connect();
        }
        if (command.CommandText.ToUpper().StartsWith("SELECT"))
        {
            return command.ExecuteReader();
        }
        return command.ExecuteNonQuery();
    }

    public SqliteDataReader ExecuteReader()
    {
        if (cnn?.State == ConnectionState.Closed)
        {
            Connect();
        }

        return command.ExecuteReader();
    }

    public dynamic ExecuteScalar()
    {
        if (cnn?.State == ConnectionState.Closed)
        {
            Connect();
        }
        return command.ExecuteScalar();
    }

    public void Dispose()
    {
        Disconnect();
    }

    public void CreateDatabase()
    {
        NewCommand(@"
        CREATE TABLE IF NOT EXISTS Role (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        role TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS User (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            email TEXT NOT NULL,
            password TEXT NOT NULL,
            name TEXT NOT NULL,
            roleID INTEGER,
            FOREIGN KEY (roleID) REFERENCES Role(id)
        );

        CREATE TABLE IF NOT EXISTS Product (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            price DECIMAL(10, 2) NOT NULL
        );

        CREATE TABLE IF NOT EXISTS Purchase (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            userID INTEGER,
            orderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
            total DECIMAL(10, 2),
            FOREIGN KEY (userID) REFERENCES User(id)
        );

        CREATE TABLE IF NOT EXISTS PurchaseItem (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            purchaseID INTEGER NOT NULL,
            productID INTEGER NOT NULL,
            quantity INTEGER NOT NULL,
            FOREIGN KEY (purchaseID) REFERENCES Purchase(id) ON DELETE CASCADE,
            FOREIGN KEY (productID) REFERENCES Product(id) ON DELETE CASCADE
        );");
        Connect();
        Execute();
        Dispose();
    }
}