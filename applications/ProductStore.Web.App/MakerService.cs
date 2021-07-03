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

        public Maker GetById(int id)
        {
            return makerRepository.GetById(id);
        }
    }
}