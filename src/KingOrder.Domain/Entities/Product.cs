namespace KingOrder.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Gtin { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Thumb { get; set; }
        public string BarCode { get; set; }
        public bool Favorite { get; set; }
    }
}
