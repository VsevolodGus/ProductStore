using System.Collections.Generic;

namespace Store.Data;

public class OrderEntity
{
    public int ID { get; init; }

    public string CellPhone { get; set; }

    public string Email { get; set; }

    public string DeliveryUniqueCode { get; set; }

    public string DeliveryDescription { get; set; }

    public decimal DeliveryPrice { get; set; }

    public Dictionary<string, string> DeliveryParameters { get; set; }

    public string PaymentUniqueCode { get; set; }

    public string PaymentDescription { get; set; }

    public Dictionary<string, string> PaymentParameters { get; set; }

    public ICollection<OrderItemEntity> Items { get; set; } = new List<OrderItemEntity>();
}
