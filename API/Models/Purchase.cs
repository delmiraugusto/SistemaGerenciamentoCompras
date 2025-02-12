namespace API.Models;

public class Purchase
{
    public long id { get; set; } = -1;
    public int userID { get; set; } = -1;
    public DateTime orderDate { get; set; } = DateTime.MinValue;
    public decimal total { get; set; } = 0;

    public class PurchaseInsert
    {
        public int userID { get; set; } = -1;
        public DateTime orderDate { get; set; } = DateTime.UtcNow;
        public decimal total { get; set; } = 0;
    }

    public class PurchaseUpdate
    {
        public int id { get; set; } = -1;
        public int userID { get; set; } = -1;
        public DateTime orderDate { get; set; } = DateTime.UtcNow;
        public decimal total { get; set; } = 0;
    }
}