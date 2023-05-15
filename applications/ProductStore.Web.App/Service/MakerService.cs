using Store;
using Store.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App
{
    public class MakerService
    {
        private readonly IReadonlyRepository<MakerEntity> _makers;
        public MakerService(IReadonlyRepository<MakerEntity> makers)
        {
            _makers = makers;
        }

        /// <summary>
        /// Получение производителя по идентификатору
        /// </summary>
        /// <param name="id">идентификатору производителя</param>
        /// <returns>модель производителя</returns>
        public async Task<Maker> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var maker = await _makers.FirstOrDefaultAsync(c=> c.Id == id, cancellationToken);
            return Maker.Mapper.Map(maker);
        }
    }
}