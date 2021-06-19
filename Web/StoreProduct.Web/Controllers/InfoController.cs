using Microsoft.AspNetCore.Mvc;
using Store;

namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly IMakerRepository manufactureRepository;
        public InfoController(ProductService productService, IMakerRepository manufactureRepository)
        {
            this.productService = productService;
            this.manufactureRepository = manufactureRepository;
        }

        // получание продукта из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);
            var maker = manufactureRepository.GetById(product.IdMaker);

            return View("InfoProduct", (product,maker));
        }

        // получание производителя из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoManufactures(int id)
        {
            var manufacture = manufactureRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
