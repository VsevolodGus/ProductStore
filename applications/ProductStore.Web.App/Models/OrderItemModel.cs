namespace ProductStore.Web.App
{
    public class OrderItemModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public int MakerID { get; set; }

        public string MakerTitle { get; set; }

        public decimal Price { get; set; }
    }
}