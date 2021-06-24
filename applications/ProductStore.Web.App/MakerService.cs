using Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
