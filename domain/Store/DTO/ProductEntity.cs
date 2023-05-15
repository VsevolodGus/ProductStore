namespace Store.Data
{
    public class ProductEntity
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public int MakerID { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
