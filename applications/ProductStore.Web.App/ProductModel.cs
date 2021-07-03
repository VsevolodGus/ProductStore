namespace ProductStore.Web.App
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        public int MakerId { get; set; }

        public string ProductTitle { get; set; }

        public string MakerTitle { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}