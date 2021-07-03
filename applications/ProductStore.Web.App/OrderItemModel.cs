namespace ProductStore.Web.App
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

        public int MakerId { get; set; }

        public string MakerTitle { get; set; }

        public decimal Price { get; set; }
    }
}