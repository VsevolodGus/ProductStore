using System;
using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class Order
    {
        public int Id { get; set; }

        public string CellPhone { get; set; }

        public OrderItemCollection Items { get; set; }

        public int TotalCount => Items.Sum(item => item.Count);

        public decimal TotalPrice => Items.Sum(item => item.Count * item.Price) + (Delivery?.PriceDelivery ?? 0m);

        public OrderDelivery Delivery { get; set; }
        
        public OrderPayment Payment { get; set; }
        
        public Order(int id, IEnumerable<OrderItem> items)
        {
            Id = id;
            Items = new OrderItemCollection(items);
        }
    }
}
