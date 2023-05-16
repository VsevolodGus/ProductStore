﻿namespace Store.Data
{
    public class OrderItemEntity
    {
        public int ID { get; init; }

        public int ProductID { get; set; }

        public int OrderID { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }

    }
}
