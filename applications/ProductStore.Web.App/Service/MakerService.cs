using Store;

namespace ProductStore.Web.App
{
    public class MakerService
    {
        private readonly IMakerRepository makerRepository;

        public MakerService(IMakerRepository makerRepository)
        {
            this.makerRepository = makerRepository;
        }

        /// <summary>
        /// Получение производителя по идентификатору
        /// </summary>
        /// <param name="id">идентификатору производителя</param>
        /// <returns>модель производителя</returns>
        public Maker GetById(int id)
        {
            return makerRepository.GetById(id);
        }
    }
}