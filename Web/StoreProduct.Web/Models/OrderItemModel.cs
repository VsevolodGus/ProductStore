using StoreManufacture;

namespace StoreProduct.Web.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public Manufacture Manufacturer { get; set; }

        public decimal Price { get; set; }
    }
}
