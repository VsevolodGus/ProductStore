using System.Threading;
using System.Threading.Tasks;

namespace Store.IntarfaceRepositroy;
public interface IUnitOfWork
{
    public Task SaveChangeAsync(CancellationToken cancellationToken = default);
    public void SaveChange();
}
