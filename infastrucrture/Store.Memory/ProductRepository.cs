using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;

        public ProductRepository()
        {
            products.Add(new Product(1, "eggs", "red price", "eggs", 10m, ""));
            products.Add(new Product(2, "bread", "alma", "bakery", 20m, ""));
            products.Add(new Product(3, "beef", "cherkizovo", "meet", 30m, ""));
            products.Add(new Product(4, "pork", "hunter row", "meet", 40m, ""));
        }
        public List<Product> GetAllByCategory(string Category)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByManufacture(string manufacture)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
