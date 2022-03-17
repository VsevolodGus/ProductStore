using MoreLinq.Extensions;
using Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStore.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository products;
        private readonly IMakerRepository makerRepository;

        public ProductService(IProductRepository products,
                              IMakerRepository makerRepository)
        {
            this.products = products;
            this.makerRepository = makerRepository;
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await products.GetByIdAsync(id);

            return Map(product);
        }

        public async Task<List<ProductModel>> GetAllByIdMakerAsync(int id)
        {
            var list = await products.GetAllByIdMakerAsync(id);


            return list.Select(Map).ToList();
        }

        public async Task<List<ProductModel>> GetAllByQueryAsync(string query)
        {
            var listByTitle = await products.GetAllByTitleAsync(query);
            var listByCategory = await products.GetAllByCategoryAsync(query);
            var listByManufacture = await products.GetAllByManufactureAsync(query);


            var list = listByTitle.Union(listByCategory)
                                  .Union(listByManufacture)
                                  .DistinctBy(item => item.Id);
           

            return list.Select(Map).ToList();
        }

        private ProductModel Map(Product product)
        {
            return new ProductModel
            {
                ProductId = product.Id,
                MakerId = product.IdMaker,
                ProductTitle = product.Title,
                MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };
        }
    }
}
