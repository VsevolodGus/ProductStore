using System.Threading;
using System.Threading.Tasks;

namespace Store.InterfaceRepository;
public interface IUnitOfWork
{
    public Task SaveChangeAsync(CancellationToken cancellationToken = default);
    //public void SaveChange();
}
