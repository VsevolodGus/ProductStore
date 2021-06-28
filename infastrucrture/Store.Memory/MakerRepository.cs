using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Memory
{
    public class MakerRepository : IMakerRepository
    {
        private readonly DbContextFactory dbContextFactory;

        public MakerRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<List<Maker>> GetAllByTitleAsync(string title)
        {
            var dbContext = dbContextFactory.Create(typeof(MakerRepository));

            var list = await dbContext.Makers
                                      .Where(maker => maker.Title.Contains(title))
                                      .ToListAsync(); ;

            return list.Select(Maker.Mapper.Map)
                       .ToList();
        }

        public Maker GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(MakerRepository));

            var maker = dbContext.Makers
                                 .Single(m => m.Id == id);

            return Maker.Mapper.Map(maker);
        }

        public async Task<Maker>  GetByTitleAsync(string title)
        {
            var dbContext = dbContextFactory.Create(typeof(MakerRepository));

            var maker = await dbContext.Makers
                                       .SingleAsync(m => m.Title.Contains(title));

            return Maker.Mapper.Map(maker);
        }
    }
}