using System;

namespace ProductStore.Web.App;

public class Cart
{
    public Guid OrderID;

    public int TotalCount;

    public decimal TotalPrice;

    public Cart(Guid orderID, int totalCount, decimal totalPrice)
    {
        OrderID = orderID;
        TotalCount = totalCount;
        TotalPrice = totalPrice;
    }
}
