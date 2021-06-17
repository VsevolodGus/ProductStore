using Microsoft.AspNetCore.Mvc;

namespace StoreProduct.Web.Controllers
{
    public class SberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Callback()
        {
            return View();
        }
    }
}
