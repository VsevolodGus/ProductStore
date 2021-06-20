using Store;
using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository products;
        private IMakerRepository makerRepository;

        public ProductService(IProductRepository products,
                              IMakerRepository makerRepository)
        {
            this.products = products;
            this.makerRepository = makerRepository;
        }

        public ProductModel GetById(int id)
        {
            var product = products.GetById(id);

            return Map(product);
        }

        public List<ProductModel> GetAllByIdManufacture(int id)
        {
            return products.GetAllByIdManufacture(id).Select(Map).ToList();
        }

        public List<ProductModel> GetAllByQuery(string query)
        {
            var list = products.GetAllByTitle(query)
                                    .Union(products.GetAllByCategory(query))
                                    .Union(products.GetAllByManufacture(query))
                                    ?.Distinct()
                                    ?.ToList();

            return list.Select(Map).ToList();
        }

        public List<ProductModel> GetAllByIntervalPrice(decimal minPrice, decimal maxPrice)
        {
            var list = products.GetAllByPrice(minPrice, maxPrice);

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
