using System.Dynamic;
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
        userName = reader["userName"].ToString()!,
        orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
        total = decimal.Parse(reader["total"].ToString()!),
        productID = int.Parse(reader["productID"].ToString()!),
        productName = reader["productName"].ToString()!
    };


    public int Insert(PurchaseInsert obj)
    {
        using DB db = new();

        DateTime orderDate = DateTime.Now;

        db.NewCommand("INSERT INTO Purchase (userID, orderDate, total) VALUES (@userID, @orderDate, 0)");
        db.Parameter("@userID", obj.userID);
        db.Parameter("@orderDate", orderDate);
        db.Execute();

        db.NewCommand("SELECT last_insert_rowid()");
        long purchaseID = (long)db.ExecuteScalar();

        decimal total = 0;

        foreach (var item in obj.items)
        {
            db.NewCommand("INSERT INTO PurchaseItem (purchaseID, productID, quantity) VALUES (@purchaseID, @productID, @quantity)");
            db.Parameter("@purchaseID", purchaseID);
            db.Parameter("@productID", item.productID);
            db.Parameter("@quantity", item.quantity);
            db.Execute();

            db.NewCommand("SELECT price FROM Product WHERE id = @productID");
            db.Parameter("@productID", item.productID);
            decimal price = (decimal)db.ExecuteScalar();

            total += price * item.quantity;
        }

        db.NewCommand("UPDATE Purchase SET total = @total WHERE id = @purchaseID");
        db.Parameter("@total", total);
        db.Parameter("@purchaseID", purchaseID);
        db.Execute();

        return (int)purchaseID;
    }

    public List<dynamic> SelectAll()
    {
        using DB db = new();
        db.NewCommand($@"
        SELECT 
            p.id, 
            p.userID, 
            u.name AS userName, 
            pi.productID, 
            prod.name AS productName,  
            prod.price,  
            pi.quantity,  
            p.orderDate, 
            p.total
        FROM {TABLE} p
        JOIN User u ON u.id = p.userID
        JOIN PurchaseItem pi ON pi.purchaseID = p.id
        JOIN Product prod ON prod.id = pi.productID
        WHERE pi.productID > 0 AND prod.name IS NOT NULL AND prod.name != ''
        GROUP BY p.id, p.userID, p.orderDate, p.total, u.name, pi.productID, prod.name, pi.quantity, prod.price");

        List<dynamic> list = new List<dynamic>();
        Dictionary<long, Purchase> purchases = new Dictionary<long, Purchase>();

        using SqliteDataReader reader = db.ExecuteReader();
        while (reader.Read())
        {
            long purchaseId = long.Parse(reader["id"].ToString()!);

            if (!purchases.ContainsKey(purchaseId))
            {
                var purchase = new Purchase()
                {
                    id = purchaseId,
                    userID = int.Parse(reader["userID"].ToString()!),
                    userName = reader["userName"].ToString()!,
                    orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
                    total = decimal.Parse(reader["total"].ToString()!),
                    products = new List<Product>()
                };

                purchases.Add(purchaseId, purchase);
            }

            string productName = reader["productName"].ToString()!;
            if (!string.IsNullOrEmpty(productName))
            {
                var product = new Product()
                {
                    productID = int.Parse(reader["productID"].ToString()!),
                    name = productName,
                    price = decimal.Parse(reader["price"].ToString()!),
                    quantity = int.Parse(reader["quantity"].ToString()!)
                };

                purchases[purchaseId].products.Add(product);
            }
        }

        list.AddRange(purchases.Values.Select(p => new
        {
            p.id,
            p.userID,
            p.userName,
            p.orderDate,
            p.total,
            products = p.products 
        }));

        return list;
    }

    public dynamic SelectById(long id)
    {
        using DB db = new();
        db.NewCommand($@"
        SELECT 
            p.id, 
            p.userID, 
            u.name AS userName, 
            p.orderDate, 
            p.total, 
            pi.productID, 
            prod.name AS productName, 
            prod.price, 
            pi.quantity
        FROM {TABLE} p
        JOIN User u ON u.id = p.userID
        JOIN PurchaseItem pi ON pi.purchaseID = p.id
        JOIN Product prod ON prod.id = pi.productID
        WHERE p.id = @id");

        db.Parameter("@id", id);

        List<Product> products = new List<Product>();
        Purchase purchase = null;

        using SqliteDataReader reader = db.ExecuteReader();

        while (reader.Read())
        {
            if (purchase == null)
            {
                purchase = new Purchase()
                {
                    id = long.Parse(reader["id"].ToString()!),
                    userID = int.Parse(reader["userID"].ToString()!),
                    userName = reader["userName"].ToString()!,
                    orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
                    total = decimal.Parse(reader["total"].ToString()!),
                    products = products 
                };
            }

            string productName = reader["productName"].ToString()!;
            if (!string.IsNullOrEmpty(productName))
            {
                var product = new Product()
                {
                    productID = int.Parse(reader["productID"].ToString()!), 
                    name = productName,
                    price = decimal.Parse(reader["price"].ToString()!),
                    quantity = int.Parse(reader["quantity"].ToString()!)
                };

                products.Add(product);
            }
        }

        if (purchase != null)
        {
            return new
            {
                purchase.id,
                purchase.userID,
                purchase.userName,
                purchase.orderDate,
                purchase.total,
                products = purchase.products 
            };
        }

        return new { }; 
    }



    public List<dynamic> SelectByUserId(long userID)
    {
        using DB db = new();
        db.NewCommand($@"
        SELECT 
            p.id,
            p.userID,
            u.name AS userName,
            p.orderDate,
            p.total,
            pi.productID,
            prod.name AS productName,
            prod.price AS productPrice,
            pi.quantity
        FROM {TABLE} p
            JOIN User u ON u.id = p.userID
            JOIN PurchaseItem pi ON pi.purchaseID = p.id
            JOIN Product prod ON prod.id = pi.productID 
        WHERE p.userID = @userID");

        db.Parameter("@userID", userID);
        List<dynamic> list = new List<dynamic>();
        Dictionary<long, Purchase> purchases = new Dictionary<long, Purchase>();

        using SqliteDataReader reader = db.ExecuteReader();
        while (reader.Read())
        {
            long purchaseId = long.Parse(reader["id"].ToString()!);

            if (!purchases.ContainsKey(purchaseId))
            {
                var purchase = new Purchase()
                {
                    id = purchaseId,
                    userID = int.Parse(reader["userID"].ToString()!),
                    userName = reader["userName"].ToString()!,
                    orderDate = DateTime.Parse(reader["orderDate"].ToString()!),
                    total = decimal.Parse(reader["total"].ToString()!),
                    products = new List<Product>()
                };

                purchases.Add(purchaseId, purchase);
            }

            var product = new Product()
            {
                productID = int.Parse(reader["productID"].ToString()!),
                name = reader["productName"].ToString()!,
                price = decimal.Parse(reader["productPrice"].ToString()!),
                quantity = int.Parse(reader["quantity"].ToString()!) 
            };

            purchases[purchaseId].products.Add(product);
        }

        foreach (var purchase in purchases.Values)
        {
            dynamic purchaseResult = new ExpandoObject();
            purchaseResult.id = purchase.id;
            purchaseResult.userID = purchase.userID;
            purchaseResult.userName = purchase.userName;
            purchaseResult.orderDate = purchase.orderDate;
            purchaseResult.total = purchase.total;

            purchaseResult.products = new List<dynamic>();
            foreach (var product in purchase.products)
            {
                dynamic productResult = new ExpandoObject();
                productResult.id = product.productID;
                productResult.name = product.name;
                productResult.price = product.price;
                productResult.quantity = product.quantity;

                purchaseResult.products.Add(productResult);
            }

            list.Add(purchaseResult);
        }

        return list;
    }








    public int UpdateById(PurchaseUpdate obj, long id)
    {
        using DB db = new();
        db.NewCommand($"UPDATE {TABLE} SET userID=@userID, orderDate=@orderDate, total=@total WHERE id = @id");
        db.Parameter("@id", id);
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
