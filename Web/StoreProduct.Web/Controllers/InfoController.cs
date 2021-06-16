using Microsoft.AspNetCore.Mvc;
using Store;
using StoreManufacture


namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly IMakerRerpository manufactureRepository;
        public InfoController(ProductService productService, IMakerRerpository manufactureRepository)
        {
            this.productService = productService;
            this.manufactureRepository = manufactureRepository;
        }

        // получание продукта из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);

            return View("InfoProduct", product);
        }

        // получание производителя из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoManufactures(int id)
        {
            var manufacture = manufactureRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
