namespace Store.Data
{
    public class OrderItemEntity
    {
        public int Id { get; init; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public OrderEntity Order { get; set; }

    }
}
