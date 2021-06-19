using System.Collections.Generic;
using System.Linq;

namespace Store
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMakerRepository makerRepository;

        public ProductService(IProductRepository productRepository,
                              IMakerRepository makerRepository)
        {
            this.productRepository = productRepository;
            this.makerRepository = makerRepository;
        }

        public Product GetById(int id)
        {
            var product = productRepository.GetById(id);

            return product;
        }

        public List<Product> GetAllByIdManufacture(int id)
        {
            return productRepository.GetAllByIdManufacture(id);
        }

        public List<Product> GetAllByQuery(string query)
        {
            var list = productRepository.GetAllByTitle(query)
                                    .Union(productRepository.GetAllByCategory(query))
                                    .Union(productRepository.GetAllByManufacture(query))
                                    .Distinct()
                                    .ToList();

            return list;
        }

        public List<Product> GetAllByIntervalPrice(decimal minPrice, decimal maxPrice)
        {
            var list = productRepository.GetAllByPrice(minPrice, maxPrice);

            return list;
        }
    }
}
