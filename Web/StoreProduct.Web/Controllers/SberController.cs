using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
