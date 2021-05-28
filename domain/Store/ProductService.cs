using StoreManufacture;
using System.Collections.Generic;
using System.Linq;

namespace Store
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
            var product = products.GetAllById(id);

            return product;
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            return products.GetAllByIdManufacture(id);
        }

        public List<Product> GetAllByQuery(string query)
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
    }
}
