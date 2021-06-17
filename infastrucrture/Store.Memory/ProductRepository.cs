using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMakerRepository manufactures = new MakerRepository();
        private readonly List<Product> products = new List<Product>();
        public ProductRepository()
        {
            using (var db = new StoreContext())
            {
                //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
                //db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
                //db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
                //db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));

                db.SaveChanges();

                products = db.Products.ToList();

                foreach (var e in products)
                {
                    e.Manufacture = manufactures.GetById(e.IdMaker);
                }
            }
        }


        public Product GetById(int id)
        {
            return products.First(product => product.Id == id);
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
        }

        public List<Product> GetAllByTitle(string title)
        {
            var list = products.Where(product => product.Title == title).ToList();
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
