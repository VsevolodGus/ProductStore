using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProductStore.Web.App;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IActionResult> Index(string query)
        {
            var model = await productService.GetAllByQueryAsync(query);
            if (model == null || model.Count == 0)
                return View("EmptySearch");

            return View(model);
        }

        // получение списка продуктов определнного производителя, по Id Maker/Manifacture
        public async Task<IActionResult> GetAllProductsMakers(int queryMaker)
        {
            var model = await productService.GetAllByIdMakerAsync(queryMaker);
            if (model == null || model.Count == 0)
                return View("EmptySearch");

            return View("Index", model);
        }
    }
}
