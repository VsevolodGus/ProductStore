using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMakerRepository makerRepository = new MakerRepository();
        private readonly List<Product> products = new List<Product>();
        public ProductRepository()
        {
                //db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
                //db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
                //db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
                //db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
                //db.SaveChanges();
        }

        public List<Product> GetAllByCategory(string сategory)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllByIds(IEnumerable<int> productIds)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllByManufacture(string title)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllByPrice(decimal minPrice, decimal maxPrice)
        {
            throw new System.NotImplementedException();
        }

        public List<Product> GetAllByTitle(string title)
        {
            throw new System.NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
