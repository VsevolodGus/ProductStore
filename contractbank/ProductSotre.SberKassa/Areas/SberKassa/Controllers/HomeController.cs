using Microsoft.AspNetCore.Mvc;
using ProductSotre.SberKassa.Areas.SberKassa.Models;

namespace ProductSotre.SberKassa.Areas.SberKassa.Controllers
{
    public class HomeController : Controller
    {
        [Area("SberKassa")]
        public IActionResult Index(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri
            };

            return View(model); ;
        }

        [Area("SberKassa")]
        public IActionResult Callback(int orderId, string returnUri)
        {
            var model = new ExampleModel
            {
                OrderId = orderId,
                ReturnUri = returnUri,
            };

            return View(model);
        }
    }
}
