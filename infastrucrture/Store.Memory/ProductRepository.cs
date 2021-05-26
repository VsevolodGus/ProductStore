using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products = new List<Product>();
        public ProductRepository()
        {
            products = new List<Product>();

            products.Add(new Product(1, "eggs", "red price", "eggs", 10m, ""));
            products.Add(new Product(2, "bread", "alma", "bakery", 20m, ""));
            products.Add(new Product(3, "beef", "cherkizovo", "meet", 30m, ""));
            products.Add(new Product(4, "pork", "hunter row", "meet", 40m, ""));
        }
        

        public Product GetAllById(int id)
        {
            return products.Single(product => product.Id == id);
        }

        public List<Product> GetAllByPrice(decimal minPrice, decimal maxPrice)
        {
            return products.Where(product => product.Price >= minPrice && product.Price <= maxPrice).ToList();
        }

        public List<Product> GetAllByCategory(string сategory)
        {
            return products.Where(item => item.Category.Contains(сategory)).ToList();
        }

        public List<Product> GetAllByManufacture(string manufacture)
        {
            return products.Where(product => product.Manufacture.Contains(manufacture)).ToList();
        }

        public List<Product> GetAllByTitle(string title)
        {
            var list = products.Where(product => product.Title.Contains(title)).ToList();
            return list;
        }
    }
}
