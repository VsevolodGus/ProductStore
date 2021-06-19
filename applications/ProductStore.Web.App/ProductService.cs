using Store;
using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Web.App
{
    public class ProductService
    {
        private readonly IProductRepository products;

        public ProductService(IProductRepository products)
        {
            this.products = products;
        }

        public Product GetById(int id)
        {
            var product = products.GetById(id);

            return product;
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            return products.GetAllByIdManufacture(id);
        }

        public IReadOnlyCollection<Product> GetAllByQuery(string query)
        {
            var list = products.GetAllByTitle(query)
                                    .Union(products.GetAllByCategory(query))
                                    .Union(products.GetAllByManufacture(query))
                                    .Distinct()
                                    .ToList();

            return list;
        }

        public List<Product> GetAllByIntervalPrice(decimal minPrice, decimal maxPrice)
        {
            var list = products.GetAllByPrice(minPrice, maxPrice);

            return list;
        }

        private ProductModel Map(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                IdMaker = product.IdMaker,
                Description = product.Description,
                Price = product.Price,
            };
        }
    }
}
