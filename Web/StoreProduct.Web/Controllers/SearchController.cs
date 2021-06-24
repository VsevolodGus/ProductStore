using Microsoft.AspNetCore.Mvc;
using Store;
using System.Collections.Generic;
using ProductStore.Web.App;

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
            var model = new List<ProductModel>(productService.GetAllByQuery(query));
            if (model == null || model.Count == 0)
                return View("EmptySearch");

            return View(model);
        }

        // получение списка продуктов определнного производителя, по Id Maker/Manifacture
        public IActionResult AllProductsMakers(int IdManufacture)
        {
            var model = new List<ProductModel>(productService.GetAllByIdManufacture(IdManufacture));
            if (model == null || model.Count == 0)
                return View("EmptySearch");

            return View("Index", model);
        }
    }
}
