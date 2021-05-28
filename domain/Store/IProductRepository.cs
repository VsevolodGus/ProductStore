using System.Collections.Generic;

namespace Store
{
    public interface IProductRepository
    {
        Product GetAllById(int id);

        List<Product> GetAllByIds(IEnumerable<int> productIds);

        List<Product> GetAllByManufacture(string title);

        List<Product> GetAllByTitle(string title);

        List<Product> GetAllByCategory(string сategory);

        List<Product> GetAllByPrice(decimal minPrice, decimal maxPrice);

        List<Product> GetAllByIdManufacture(int id);

    }
}
