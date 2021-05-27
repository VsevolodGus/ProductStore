using Microsoft.AspNetCore.Mvc;
using Store;
using StoreProduct.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StoreProduct.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public OrderController(IProductRepository productRepository,
                               IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                return View("Index","Home");
            }

            return View("Empty");
        }

        [HttpPost]
        public IActionResult AddItem(Product products,int id)
        {
            var product = productRepository.GetAllById(id);


            return RedirectToAction("AgainIndex", "Search", products);
        }
    }
}
