﻿using StoreManufacture;

namespace Store
{

    public class Product
    {
        public int Id { get; }

        public string Title { get; }

        public Maker Manufacture { get; }

        public string Category { get; }

        public decimal Price { get; }

        public string Description { get; }

        public Product(int id, string title, Maker manufacture, string category, decimal price, string description)
        {
            Id = id;
            Title = title;
            Category = category;
            Manufacture = manufacture;
            Price = price;
            Description = description;
        }

        public Product() { }
    }
}
