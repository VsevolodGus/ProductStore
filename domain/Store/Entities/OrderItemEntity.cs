using System;

namespace Store.Entities;

public class OrderItemEntity
{
    public int ID { get; init; }

    public int ProductID { get; set; }

    public Guid OrderID { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public OrderEntity Order { get; set; }
    public ProductEntity Product { get; set; }

}
