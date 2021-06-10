using StoreManufacture;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ManufactureRepository : IMakerRerpository
    {
        private readonly List<Maker> manufactures;

        public ManufactureRepository()
        {
            manufactures = new List<Maker>();

            manufactures.Add(new Maker(1, "red price", "", "", "weqw", ""));
            manufactures.Add(new Maker(2, "alma", "", "", "qwer", ""));
            manufactures.Add(new Maker(3, "cherkizovo", "", "", "qdswer", ""));
            manufactures.Add(new Maker(4, "hunter row", "", "", "qwqrdff", ""));

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
