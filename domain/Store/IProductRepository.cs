using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        List<Product> GetAllByManufacture(string manufacture);

        List<Product> GetAllByTitle(string title);

        List<Product> GetAllByCategory(string Category);
    }
}
