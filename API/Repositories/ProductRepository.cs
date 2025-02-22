using API.Core;
using API.Models;
using Microsoft.Data.Sqlite;

namespace API.Repositories;

public class ProductRepository : IRepository<ProductInsert, ProductUpdate>
{
    public string TABLE => "Product";
    public dynamic SetAttributes(SqliteDataReader reader) => new Product()
    {
        productID = long.Parse(reader["id"].ToString()!),
        name = reader["name"].ToString()!,
        price = decimal.Parse(reader["price"].ToString()!),
    };
    public int Insert(ProductInsert obj)
    {
        using DB db = new();
        db.NewCommand($"INSERT INTO {TABLE} (name, price) VALUES (@name, @price)");
        db.Parameter("@name", obj.name);
        db.Parameter("@price", obj.price);
        return db.Execute();
    }
    public List<dynamic> SelectAll()
    {
        using DB db = new();
        db.NewCommand($"SELECT id, name, price FROM {TABLE}");

        List<dynamic> list = [];
        using SqliteDataReader reader = db.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new
            {
                id = reader["id"],
                name = reader["name"],
                price = reader["price"]
            });
        }

        return list;
    }

    public dynamic SelectById(long id)
    {
        using DB db = new();
        db.NewCommand($"SELECT id, name, price FROM {TABLE} WHERE id = @id");
        db.Parameter("@id", id);

        using SqliteDataReader reader = db.ExecuteReader();
        if (reader.Read())
        {
            return new
            {
                id = reader["id"],
                name = reader["name"],
                price = reader["price"]
            };
        }

        return new Product();
    }

    public int UpdateById(ProductUpdate obj, long id)
    {
        using DB db = new();
        db.NewCommand($"UPDATE {TABLE} SET name=@name, price=@price WHERE id = @id");
        db.Parameter("@id", id);
        db.Parameter("@name", obj.name);
        db.Parameter("@price", obj.price);
        return db.Execute();
    }
    public int DeleteById(long id)
    {
        using DB db = new();
        db.NewCommand($"DELETE FROM {TABLE} WHERE id = @id");
        db.Parameter("@id", id);
        return db.Execute();
    }
}