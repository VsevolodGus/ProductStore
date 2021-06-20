using System.Collections.Generic;


namespace ProductStore.Web.App
{
    public class OrderModel
    {
        public int Id { get; set; }

        public List<OrderItemModel> Items = new List<OrderItemModel>();

        public string CellPhone { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}
