    using API.Core;
using API.Models;
using Microsoft.Data.Sqlite;
using static API.Models.Purchase;

namespace API.Repositories;

public class PurchaseRepository : IRepository<PurchaseInsert, PurchaseUpdate>
{
    public string TABLE => "Purchase";

    public dynamic SetAttributes(SqliteDataReader reader) => new Purchase()
    {
        id = long.Parse(reader["id"].ToString()!),
        userID = int.Parse(reader["userID"].ToString()!),
        productID = int.Parse(reader["productID"].ToString()!),
        productName = reader["productName"].ToString()!,
        userName = reader["userName"].ToString()!,
        orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
        total = decimal.Parse(reader["total"].ToString()!)
    };

    public int Insert(PurchaseInsert obj)
    {
        using DB db = new();
        db.NewCommand($"INSERT INTO {TABLE} (userID, productID, orderDate, total) VALUES (@userID, @productID, @orderDate, @total)");
        db.Parameter("@userID", obj.userID);
        db.Parameter("@userID", obj.userID);
        db.Parameter("@orderDate", obj.orderDate);
        db.Parameter("@total", obj.total);
        return db.Execute();
    }

    public List<dynamic> SelectAll()
    {
        using DB db = new();
        db.NewCommand($"SELECT p.id, p.userID, p.productID, prod.name AS productName, u.name AS userName, p.orderDate, p.total " +
              $"FROM {TABLE} p " +
              $"JOIN User u ON u.id = p.userID " +
              $"JOIN Product prod ON prod.id = p.productID");
        List<dynamic> list = [];
        using SqliteDataReader reader = db.Execute();
        while (reader.Read())
        {
            list.Add(SetAttributes(reader));
        }
        return list;
    }

    public dynamic SelectById(long id)
    {
        using DB db = new();
        db.NewCommand($"SELECT p.id, p.userID, p.productID, prod.name AS productName, u.name AS userName, p.orderDate, p.total " +
              $"FROM {TABLE} p " +
              $"JOIN User u ON u.id = p.userID " +
              $"JOIN Product prod ON prod.id = p.productID WHERE p.id = @id");
        db.Parameter("@id", id);
        using SqliteDataReader reader = db.Execute();
        if (reader.Read()) return SetAttributes(reader);
        return new Purchase();
    }

    public List<dynamic> SelectByUserId(long userID)
    {
        using DB db = new();
        db.NewCommand($"SELECT p.id, p.userID, p.productID, prod.name AS productName, u.name AS userName, p.orderDate, p.total " +
              $"FROM {TABLE} p " +
              $"JOIN User u ON u.id = p.userID " +
              $"JOIN Product prod ON prod.id = p.productID WHERE p.userID = @userID");
        db.Parameter("@userID", userID);
        List<dynamic> list = [];
        using SqliteDataReader reader = db.Execute();
        while (reader.Read())
        {
            list.Add(SetAttributes(reader));
        }
        return list;
    }

    public int UpdateById(PurchaseUpdate obj, long id)
    {
        using DB db = new();
        db.NewCommand($"UPDATE {TABLE} SET userID=@userID, productID=@productID, orderDate=@orderDate, total=@total WHERE id = @id");
        db.Parameter("@id", id);
        db.Parameter("@userID", obj.userID);
        db.Parameter("@productID", obj.productID);
        db.Parameter("@orderDate", obj.orderDate);
        db.Parameter("@total", obj.total);
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
