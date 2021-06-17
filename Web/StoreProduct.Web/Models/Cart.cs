namespace StoreProduct.Web.Models
{
    public class Cart
    {
        public int OrderId;

        public int TotalCount;

        public decimal TotalPrice;

        public Cart(int id)
        {
            OrderId = id;

            TotalCount = 0;
            TotalPrice = 0m;
        }
    }
}
