namespace API.Models;

public class PurchaseItem
{
    public long id { get; set; } = -1;
    public int productID { get; set; } = -1;
    public int purchaseID { get; set; } = -1;
    public int quantity { get; set; } = -1;

    public class PurchaseItemInsert
    {
        public int productID { get; set; } = -1;
        public int purchaseID { get; set; } = -1;
        public int quantity { get; set; } = -1;
    }

    public class PurchaseItemUpdate
    {
        public int productID { get; set; } = -1;
        public int purchaseID { get; set; } = -1;
        public int quantity { get; set; } = -1;
    }
}
