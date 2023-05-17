using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App.Service;
public interface IProductService
{
    /// <summary>
    /// Получение модели продукта по идентификатору
    /// </summary>
    /// <param name="id">идентификатор продукта</param>
    /// <returns>модель продукта</returns>
    Task<ProductModel> GetByIDAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение продуктов по идентификатору
    /// </summary>
    /// <param name="makerID">идентификатор производителя</param>
    /// <returns>список моделей продуктов</returns>
    Task<ProductModel[]> GetAllByIDMakerAsync(int makerID, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение списка продуктов
    /// </summary>
    /// <param name="search">строка поиска</param>
    /// <returns>список моделей продуктов</returns>
    Task<ProductModel[]> GetAllByQueryAsync(string search, CancellationToken cancellationToken = default);
}
