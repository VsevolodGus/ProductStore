using ProductStore.Web.App.Service;
using Store;
using Store.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App;



internal class MakerService : IMakerService
{
    private readonly IReadonlyRepository<MakerEntity> _makers;
    public MakerService(IReadonlyRepository<MakerEntity> makers)
    {
        _makers = makers;
    }

    public async Task<Maker> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var maker = await _makers.FirstOrDefaultAsync(c=> c.ID == id, cancellationToken);
        return Maker.Mapper.Map(maker);
    }
}