using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class MakerRepository : IMakerRepository
    {
        private readonly List<Maker> manufactures;

        public MakerRepository()
        {
            //manufactures = new List<Maker>();

            using (var db = new StoreContext())
            {

                //db.Makers.Add(new Maker("Красная Цена", "8937-216-76-11", "",
                //    "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!"));
                //db.Makers.Add(new Maker("АЛМА", "8347-827-36-96", "",
                //    "ул.Комарова, д.41;", " У нас вы найдете экологически чистые продукты по приятным ценам"));
                //db.Makers.Add(new Maker("Черкизово", "+7495-660-24-40", " sk@cherkizovo.com",
                //    "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", ""));
                //db.Makers.Add(new Maker("Мясницкий ряд", " +7495-411-33-41", " zakupki@kolbasa.ru",
                //    "Транспортный проезд д.7 г. Одинцово Московская область", ""));

                db.SaveChanges();

                manufactures = db.Makers.ToList();
            }
        }

        public Maker GetById(int id)
        {
            return manufactures.Single(item => item.Id == id);
        }

        public Maker GetByTitle(string title)
        {
            return manufactures.Single(item => item.Title == title);
        }
    }
}
