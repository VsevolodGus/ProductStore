using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        Product GetAllById(int id);

        List<Product> GetAllByManufacture(string manufacture);

        List<Product> GetAllByTitle(string title);

        List<Product> GetAllByCategory(string сategory);

        List<Product> GetAllByPrice(decimal minPrice, decimal maxPrice);
    }
}
