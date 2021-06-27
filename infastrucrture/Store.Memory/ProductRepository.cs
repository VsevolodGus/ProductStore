using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMakerRepository makerRepository;
        private readonly DbContextFactory dbContextFactory;

        public ProductRepository( IMakerRepository makerRepository,
                                  DbContextFactory dbContextFactory)
        {
            this.makerRepository = makerRepository;
            this.dbContextFactory = dbContextFactory;
        }
        public List<Product> GetAllByCategory(string сategory)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var list = dbContext.Products.Where(product => product.Category.Contains(сategory));

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var list = dbContext.Products.Where(product => product.IdMaker == id);

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public List<Product> GetAllByIds(IEnumerable<int> productIds)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));
            
            var list = dbContext.Products.Where(product => productIds.Contains(product.Id));

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public List<Product> GetAllByManufacture(string title)
        {
            var listMaker = makerRepository.GetAllByTitle(title);
            var idsMakers = listMaker.Select(maker => maker.Id);

            var dbContextProduct = dbContextFactory.Create(typeof(ProductRepository));
            var ProductList = dbContextProduct.Products.Where(product => idsMakers.Contains(product.Id));

            return ProductList.Select(Product.Mapper.Map)
                       .ToList();
        }

        public List<Product> GetAllByTitle(string title)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));
            
            var list = dbContext.Products.Where(product => product.Title.Contains(title));

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public Product GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var product = dbContext.Products.Single(product => product.Id ==id);

            return Product.Mapper.Map(product);
        }
    }
}
//db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
//db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
//db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
//db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
