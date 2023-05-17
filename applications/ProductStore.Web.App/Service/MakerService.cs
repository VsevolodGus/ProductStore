using ProductStore.Web.App.Service;
using Store;
using Store.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App;

internal class MakerService : IMakerService
{
    private readonly IReadonlyRepository<PublishingHouseEntity> _makers;
    public MakerService(IReadonlyRepository<PublishingHouseEntity> makers)
    {
        _makers = makers;
    }

    public async Task<Maker> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var maker = await _makers.FirstOrDefaultAsync(c=> c.ID == id, cancellationToken);
        return Maker.Mapper.Map(maker);
    }
}