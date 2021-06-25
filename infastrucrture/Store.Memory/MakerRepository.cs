using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class MakerRepository : IMakerRepository
    {

        //db.Makers.Add(new Maker("Красная Цена", "8937-216-76-11", "",
        //    "ООО 'Красная Цена' Самара", "Название Красная Цена выбрано не случайно!"));
        //db.Makers.Add(new Maker("АЛМА", "8347-827-36-96", "",
        //    "ул.Комарова, д.41;", " У нас вы найдете экологически чистые продукты по приятным ценам"));
        //db.Makers.Add(new Maker("Черкизово", "+7495-660-24-40", " sk@cherkizovo.com",
        //    "Москва 125047 Лесная улица 5Б, бизнес - центр «Белая площадь», 12 - й этаж", ""));
        //db.Makers.Add(new Maker("Мясницкий ряд", " +7495-411-33-41", " zakupki@kolbasa.ru",
        //    "Транспортный проезд д.7 г. Одинцово Московская область", ""));
        public Maker GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Maker GetByTitle(string title)
        {
            throw new System.NotImplementedException();
        }
    }
}
