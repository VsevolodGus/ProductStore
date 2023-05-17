using System;
using System.Collections.Generic;

namespace Store.Entities;

public enum StatusOrder : byte
{
    InProgress = 0,
    Cancel = 1,
    Confirmed = 2,
    WaitingPayment = 3,
    Finish = 4,
}

public class OrderEntity
{

    public OrderEntity()
    {
        ID = Guid.NewGuid();
    }

    public Guid ID { get; init; }

    public string CellPhone { get; set; }

    public string Email { get; set; }

    public string DeliveryUniqueCode { get; set; }

    public string DeliveryDescription { get; set; }

    public decimal DeliveryPrice { get; set; }

    public Dictionary<string, string> DeliveryParameters { get; set; }

    public string PaymentUniqueCode { get; set; }

    public string PaymentDescription { get; set; }

    public Dictionary<string, string> PaymentParameters { get; set; }

    public short StatusOrder { get; set; }

    public ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
}
