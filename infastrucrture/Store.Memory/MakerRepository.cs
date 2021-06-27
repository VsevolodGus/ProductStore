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
//db.Makers.Add(new Maker("Красная Цена", "8937-216-76-11", "",
//    "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!"));
//db.Makers.Add(new Maker("АЛМА", "8347-827-36-96", "",
//    "ул.Комарова, д.41;", " У нас вы найдете экологически чистые продукты по приятным ценам"));
//db.Makers.Add(new Maker("Черкизово", "+7495-660-24-40", " sk@cherkizovo.com",
//    "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", ""));
//db.Makers.Add(new Maker("Мясницкий ряд", " +7495-411-33-41", " zakupki@kolbasa.ru",
//    "Транспортный проезд д.7 г. Одинцово Московская область", ""));