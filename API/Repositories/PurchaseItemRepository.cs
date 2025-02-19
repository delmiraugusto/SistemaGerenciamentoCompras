using API.Core;
using API.Models;
using Microsoft.Data.Sqlite;
using static API.Models.PurchaseItem;

namespace API.Repositories;

public class PurchaseItemRepository : IRepository<PurchaseItemInsert, PurchaseItemUpdate>
{
        public string TABLE => "PurchaseItem";
        public dynamic SetAttributes(SqliteDataReader reader) => new PurchaseItem()
        {
            id = long.Parse(reader["id"].ToString()!),
            productID = int.Parse(reader["productID"].ToString()!),
            purchaseID = int.Parse(reader["purchaseID"].ToString()!),
            quantity = int.Parse(reader["quantity"].ToString()!),
        };

        public int Insert(PurchaseItemInsert obj)
        {
            using DB db = new();
            db.NewCommand($"INSERT INTO {TABLE} (productID, purchaseID, quantity) VALUES (@productID, @purchaseID, @quantity)");
            db.Parameter("@productID", obj.productID);
            db.Parameter("@purchaseID", obj.purchaseID);
            db.Parameter("@quantity", obj.quantity);
            return db.Execute();
        }

        public List<dynamic> SelectAll()
        {
            using DB db = new();
            db.NewCommand($"SELECT id, productID, purchaseID, quantity FROM {TABLE}");
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
            db.NewCommand($"SELECT id, productID, purchaseID, quantity FROM {TABLE} WHERE id = @id");
            db.Parameter("@id", id);
            using SqliteDataReader reader = db.Execute();
            if (reader.Read()) return SetAttributes(reader);
            return new Product();
        }

        public int UpdateById(PurchaseItemUpdate obj, long id)
        {
            using DB db = new();
            db.NewCommand($"UPDATE {TABLE} SET productID=@productID, purchaseID=@purchaseID, quantity=@quantity WHERE id = @id");
            db.Parameter("@id", id);
            db.Parameter("@productID", obj.productID);
            db.Parameter("@purchaseID", obj.purchaseID);
            db.Parameter("@quantity", obj.quantity);
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
