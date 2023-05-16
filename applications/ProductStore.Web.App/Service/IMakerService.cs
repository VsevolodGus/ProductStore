using Store;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App.Service;
public interface IMakerService
{
    /// <summary>
    /// Получение производителя по идентификатору
    /// </summary>
    /// <param name="id">идентификатору производителя</param>
    /// <returns>модель производителя</returns>
    Task<Maker> GetByIdAsync(int id, CancellationToken cancellationToken);
}
