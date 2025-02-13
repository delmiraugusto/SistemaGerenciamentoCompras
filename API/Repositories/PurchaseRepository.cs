﻿    using API.Core;
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
        name = reader["name"].ToString()!,
        orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
        total = decimal.Parse(reader["total"].ToString()!)
    };

    public int Insert(PurchaseInsert obj)
    {
        using DB db = new();
        db.NewCommand($"INSERT INTO {TABLE} (userID, orderDate, total) VALUES (@userID, @orderDate, @total)");
        db.Parameter("@userID", obj.userID);
        db.Parameter("@orderDate", obj.orderDate);
        db.Parameter("@total", obj.total);
        return db.Execute();
    }

    public List<dynamic> SelectAll()
    {
        using DB db = new();
        db.NewCommand($"SELECT p.id, p.userID, u.name, p.orderDate, p.total FROM {TABLE} p JOIN User u on u.id = p.userID");
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
        db.NewCommand($"SELECT p.id, p.userID, u.name, orderDate, total FROM {TABLE} p join User u on u.id = p.userID WHERE p.id = @id");
        db.Parameter("@id", id);
        using SqliteDataReader reader = db.Execute();
        if (reader.Read()) return SetAttributes(reader);
        return new Purchase();
    }

    public List<dynamic> SelectByUserId(long userID)
    {
        using DB db = new();
        db.NewCommand($"SELECT p.id, p.userID, u.name, orderDate, total FROM {TABLE} p join User u on u.id = p.userID WHERE p.userID = @userID");
        db.Parameter("@userID", userID);
        List<dynamic> list = [];
        using SqliteDataReader reader = db.Execute();
        while (reader.Read())
        {
            list.Add(SetAttributes(reader));
        }
        return list;
    }

    public int UpdateById(PurchaseUpdate obj)
    {
        using DB db = new();
        db.NewCommand($"UPDATE {TABLE} SET userID=@userID, orderDate=@orderDate, total=@total WHERE id = @id");
        db.Parameter("@id", obj.id);
        db.Parameter("@userID", obj.userID);
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
