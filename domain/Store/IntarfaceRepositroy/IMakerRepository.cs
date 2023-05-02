using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IMakerRepository
    {
        /// <summary>
        /// Получение производителя по идентификатору
        /// </summary>
        /// <param name="id">идентификатор производителя</param>
        /// <returns>производитель</returns>
        Maker GetById(int id);

        /// <summary>
        /// Получение списка производителей с поиском по названию
        /// </summary>
        /// <param name="title">строка поиска</param>
        /// <returns>список сущностей производителей</returns>
        Task<List<Maker>> GetAllByTitleAsync(string title);
    }
}
