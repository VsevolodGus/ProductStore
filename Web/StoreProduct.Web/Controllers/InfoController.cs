using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly MakerService makerService;
        public InfoController(ProductService productService,
                              MakerService makerService)
        {
            this.productService = productService;
            this.makerService = makerService;
        }

        // получание продукта из репозитория и вывод его полей, запрос по Id
        public async Task<IActionResult> InfoProduct(int id)
        {
            var model = await productService.GetByIdAsync(id);

            return View("InfoProduct", model);
        }

        // получание производителя из репозитория и вывод его полей, запрос по Id
        public IActionResult InfoManufacture(int id)
        {
            var manufacture = makerService.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
