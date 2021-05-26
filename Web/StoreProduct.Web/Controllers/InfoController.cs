using Microsoft.AspNetCore.Mvc;
using Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers
{
    public class InfoController : Controller
    {
        private readonly ProductService productService;

        public InfoController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult InfoProduct(int id)
        {
            var product = productService.GetById(id);
            return View("InfoProduct", product);
        }

        //public IActionResult InfoManufactures(int id)
        //{
        //    return View();
        //}
    }
}
