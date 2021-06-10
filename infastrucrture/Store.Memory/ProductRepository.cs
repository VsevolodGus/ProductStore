using StoreManufacture;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMakerRerpository manufactures = new ManufactureRepository();
        private readonly List<Product> products = new List<Product>();
        public ProductRepository()
        {
            using(var db = new StoreContext())
            {
                db.Products.Add(new Product( "яйца", manufactures.GetById(1), "яйцв", 10m, "куринные яйца, категории C0"));
                db.Products.Add(new Product( "хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
                db.Products.Add(new Product( "говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
                db.Products.Add(new Product( "свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));

                db.SaveChanges();

                products = db.Products.ToList();

                int i = 0;
                i++;
            }

            //products.Add(new Product(1, "eggs", new Maker(1, "red price", "", "", "", ""), "eggs", 10m, ""));
            //products.Add(new Product(2, "bread", new Maker(2, "alma", "", "", "", ""), "bakery", 20m, ""));
            //products.Add(new Product(3, "beef", new Maker(3, "cherkizovo", "", "", "", ""), "meet", 30m, ""));
            //products.Add(new Product(4, "pork", new Maker(4, "hunter row", "", "", "", ""), "meet", 40m, ""));
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
            return products.Where(product => product.Manufacture.Title.Contains(manufacture)).ToList();
            //throw new NotImplementedException();
        }

        public List<Product> GetAllByTitle(string title)
        {
            var list = products.Where(product => product.Title.Contains(title)).ToList();
            return list;
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            return this.GetAllByManufacture(manufactures.GetById(id).Title);
        }

        public List<Product> GetAllByIds(IEnumerable<int> productIds)
        {
            var productsList = from product in products
                               join productId in productIds on product.Id equals productId
                               select product;

            return productsList.ToList();
        }
    }
}
