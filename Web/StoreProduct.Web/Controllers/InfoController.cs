using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Memory;


namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;
        private readonly ManufactureRepository manufactureRepository;
        public InfoController(ProductService productService, ManufactureRepository manufactureRepository)
        {
            this.productService = productService;
            this.manufactureRepository = manufactureRepository;
        }

        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);

            return View("InfoProduct", product);
        }

        public IActionResult InfoManufactures(int id)
        {
            var manufacture = manufactureRepository.GetById(id);

            return View("InfoManufacture", manufacture);
        }
    }
}
