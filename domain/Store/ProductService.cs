using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Product> GetAllByQuery(string query)
        {
           var list = products.GetAllByTitle(query)
                                   .Union(products.GetAllByManufacture(query))
                                   .Union(products.GetAllByCategory(query)).Distinct().ToList();

            return list;
        }

        public List<Product> GetAllByIntervalPrice(decimal minPrice, decimal maxPrice)
        {
            var list = products.GetAllByPrice(minPrice, maxPrice);

            return list;
        }
    }
}
