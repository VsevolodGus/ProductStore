using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IProductRepository
    {
        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        /// <param name="id">идентификатор продукта</param>
        /// <returns>продукт</returns>
        Task<Product> GetByIdAsync(int id);

        /// <summary>
        /// Получение продуктов по массиву идентификатору продукта
        /// </summary>
        /// <param name="productIds">массив идентификаторов продуктов</param>
        /// <returns>список продуктов</returns>
        Task<List<Product>> GetAllByIdsAsync(IEnumerable<int> productIds);

        /// <summary>
        /// Получение продуктов с поиском по имени производителя
        /// </summary>
        /// <param name="searchNameManufacturer">название производителя</param>
        /// <returns>список продуктов</returns>
        Task<List<Product>> GetAllByManufactureAsync(string searchNameManufacturer);

        /// <summary>
        /// Получение продуктов с поиском по имени производителя
        /// </summary>
        /// <param name="searchName">строка поиска</param>
        /// <returns>список продуктов</returns>
        Task<List<Product>> GetAllByTitleAsync(string searchName);

        /// <summary>
        /// Получение продуктов с поиском по категории
        /// </summary>
        /// <param name="searchCategory">строка поиска по категории</param>
        /// <returns>список продуктов</returns>
        Task<List<Product>> GetAllByCategoryAsync(string searchCategory);

        /// <summary>
        /// Получение продуктов по идентификатору производителя
        /// </summary>
        /// <param name="makerID">идентификатор производителя</param>
        /// <returns>список продуктов</returns>
        Task<List<Product>> GetAllByIdMakerAsync(int makerID);
    }
}
