using static API.Models.PurchaseItem;

namespace API.Models;

public class Purchase
{
    public long id { get; set; }
    public int userID { get; set; }
    public string userName { get; set; } = "";
    public DateTime orderDate { get; set; }
    public decimal total { get; set; }
    public int productID { get; set; }
    public string productName { get; set; } = "";
    public List<Product>? products { get; set; }

    public class PurchaseInsert
    {
        public int userID { get; set; } = -1;
        public DateTime orderDate { get; set; } = DateTime.UtcNow;
        public List<PurchaseItemInsert> items { get; set; } = [];
    }

    public class PurchaseUpdate
    {
        public int id { get; set; } = -1;
        public int userID { get; set; } = -1;
        public int productID { get; set; } = -1;
        public DateTime orderDate { get; set; } = DateTime.UtcNow;
        public decimal total { get; set; } = 0;
    }
}