namespace Store
{
    public class Product
    {
        public int Id { get; }

        public string Title { get; }

        public int IdMaker { get; }

        public string Category { get; }

        public decimal Price { get; }

        public string Description { get; }

        public Product(int id, string title,int makerId, string category, decimal price, string description)
        {
            Id = id;
            Title = title;
            Category = category;
            IdMaker = makerId;
            Price = price;
            Description = description;
        }

        public Product() { }
    }
}
