using Microsoft.AspNetCore.Mvc;
using Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProductService productService;
        public SearchController(ProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Index(string query)
        {
            var products = new List<Product>(productService.GetAllByQuery(query));
         
            return View(products);
        }
    }
}
