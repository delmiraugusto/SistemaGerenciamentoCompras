namespace API.Models
{
    public class Product
    {
        public long productID { get; set; } = -1;
        public string name { get; set; } = "";
        public decimal price { get; set; } = -1;
        public int quantity { get; set; } = 0;
    }
    public class ProductInsert
    {
        public string name { get; set; } = "";
        public decimal price { get; set; } = -1;
    }
    public class ProductUpdate
    {
        public long id { get; set; } = -1;
        public string name { get; set; } = "";
        public decimal price { get; set; } = -1;
    }
}
