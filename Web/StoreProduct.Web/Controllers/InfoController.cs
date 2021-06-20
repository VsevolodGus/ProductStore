using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App;
using Store;

namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly IMakerRepository makerRepository;

        public InfoController(ProductService productService,
                              IMakerRepository makerRepository)
        {
            this.productService = productService;
            this.makerRepository = makerRepository;
        }

        // получание продукта из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoProduct(int id)
        {
            var model = productService.GetById(id);


            return View("InfoProduct", model);
        }

        // получание производителя из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoManufacture(int id)
        {
            var manufacture = makerRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
