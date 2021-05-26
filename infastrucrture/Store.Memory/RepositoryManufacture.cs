using StoreManufacture;
using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class RepositoryManufacture : IManufactureRerpository
    {
        private readonly List<Manufacture> manufactures;

        public RepositoryManufacture()
        {
            manufactures = new List<Manufacture>();

            manufactures.Add(new Manufacture(1, "","", "", "", ""));
            manufactures.Add(new Manufacture(2, "","", "", "", ""));
            manufactures.Add(new Manufacture(3, "","", "", "", ""));

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
