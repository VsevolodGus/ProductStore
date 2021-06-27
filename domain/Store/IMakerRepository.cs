using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IMakerRepository
    {
        Maker GetById(int id);

        Task<Maker> GetByTitleAsync(string title);

        Task<List<Maker>> GetAllByTitleAsync(string title);
    }
}
