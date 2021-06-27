using System.Collections.Generic;

namespace Store
{
    public interface IMakerRepository
    {
        Maker GetById(int id);

        Maker GetByTitle(string title);

        List<Maker> GetAllByTitle(string title);
    }
}
