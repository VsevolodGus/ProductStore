namespace ProductStore.Web.App
{
    public class Cart
    {
        public int OrderId;

        public int TotalCount;

        public decimal TotalPrice;

        public Cart(int id, int totalCount, decimal totalPrice)
        {
            OrderId = id;
            TotalCount = totalCount;
            TotalPrice = totalPrice;
        }
    }
}
