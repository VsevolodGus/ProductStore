using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<List<Product>> GetAllByCategoryAsync(string сategory)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var list = await  dbContext.Products
                                       .Where(product => product.Category.Contains(сategory))
                                       .ToListAsync();

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public async Task<List<Product>> GetAllByIdMakerAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var list = await dbContext.Products
                                      .Where(product => product.IdMaker == id)
                                      .ToListAsync();

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public async Task<List<Product>> GetAllByIdsAsync(IEnumerable<int> productIds)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var list = await dbContext.Products
                                      .Where(product => productIds.Contains(product.Id))
                                      .ToListAsync();

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }

        public async Task<List<Product>> GetAllByManufactureAsync(string title)
        {
            var listMaker = await makerRepository.GetAllByTitleAsync(title);
            var idsMakers = listMaker.Select(maker => maker.Id);

            var dbContextProduct = dbContextFactory.Create(typeof(ProductRepository));
            var ProductList = await dbContextProduct.Products
                                                    .Where(product => idsMakers.Contains(product.Id))
                                                    .ToListAsync();

            return ProductList.Select(Product.Mapper.Map)
                       .ToList();
        }

        public async Task<List<Product>> GetAllByTitleAsync(string title)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));
            
            var list = await dbContext.Products
                                      .Where(product => product.Title.Contains(title))
                                      .ToListAsync();

            return list.Select(Product.Mapper.Map)
                       .ToList();
        }


        public async Task<Product> GetByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(ProductRepository));

            var product = await dbContext.Products
                                         .SingleAsync(product => product.Id == id);

            return Product.Mapper.Map(product);
        }
    }
}
//db.Products.Add(new Product("яйца", manufactures.GetById(1), "яйца", 10m, "куринные яйца, категории C0"));
//db.Products.Add(new Product("хлеб", manufactures.GetById(2), "выпечка", 20m, "хлебо-булочные изделия"));
//db.Products.Add(new Product("говядина", manufactures.GetById(3), "мясо", 30m, "мясо из говядины и телятины"));
//db.Products.Add(new Product("свинина", manufactures.GetById(4), "мясо", 40m, "мясо из свинины"));
