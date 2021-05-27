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
        public IActionResult Index(string query)
        {
            var products = new List<Product>(productService.GetAllByQuery(query));

            return View(products);
        }
    }
}
