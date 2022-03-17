using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);

        Task<List<Product>> GetAllByIdsAsync(IEnumerable<int> productIds);

        Task<List<Product>> GetAllByManufactureAsync(string title);

        Task<List<Product>> GetAllByTitleAsync(string title);

        Task<List<Product>> GetAllByCategoryAsync(string сategory);

        Task<List<Product>> GetAllByIdMakerAsync(int id);
    }
}
