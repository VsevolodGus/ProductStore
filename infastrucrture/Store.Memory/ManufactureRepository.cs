using StoreManufacture;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class ManufactureRepository : IManufactureRerpository
    {
        private readonly List<Manufacture> manufactures;

        public ManufactureRepository()
        {
            manufactures = new List<Manufacture>();

            manufactures.Add(new Manufacture(1, "red price", "", "", "weqw", ""));
            manufactures.Add(new Manufacture(2, "alma", "", "", "qwer", ""));
            manufactures.Add(new Manufacture(3, "cherkizovo", "", "", "qdswer", ""));
            manufactures.Add(new Manufacture(4, "hunter row", "", "", "qwqrdff", ""));

        }
        public Manufacture GetById(int id)
        {
            return manufactures.Single(item => item.Id == id);
        }

        public Manufacture GetByTitle(string title)
        {
            return manufactures.Single(item => item.Title == title);
        }
    }
}
