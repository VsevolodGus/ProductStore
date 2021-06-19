using Microsoft.AspNetCore.Mvc;
using Store;
using System.Collections.Generic;

namespace StoreProduct.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProductService productService;

        public SearchController(ProductService productService)
        {
            this.productService = productService;
        }

        // получение из репозитория список продуктов по запросу и вывод данных на страницу
        public IActionResult Index(string query)
        {
            var products = new List<Product>(productService.GetAllByQuery(query));

            return View(products);
        }

        // получение списка продуктов определнного производителя, по Id Maker/Manifacture
        public IActionResult AllProductsMakers(int IdManufacture)
        {
            var products = new List<Product>(productService.GetAllByIdManufacture(IdManufacture));

            return View("Index", products);
        }
    }
}
