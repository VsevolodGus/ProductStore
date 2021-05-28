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

        [HttpGet]
        public IActionResult Index()
        {
            if (!HttpContext.Session.TryGetCart(out Cart cart))
            {
                return View("Index","Home");
            }

            return View("Empty");
        }

        private (Order order, Cart cart) GetCreateOrderAndCart()
        {
            Order order;

            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.GetById(cart.OrderId);
            }
            else
            {
                order = orderRepository.Create();
                cart = new Cart(order.Id);
            }

            return (order, cart);
        }

        private void SaveOrderAndCart(Order order, Cart cart)
        {
            orderRepository.Update(order);

            cart.TotalCount = order.TotalCount;
            cart.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cart);
        }

        [HttpPost]
        public IActionResult AddItem(int id)
        {
            (Order order, Cart cart) = GetCreateOrderAndCart();

            var product = productRepository.GetAllById(id);

            order.AddOrUpdate(product, 1);

            SaveOrderAndCart(order,cart);

            return RedirectToAction("InfoProduct", "Info", product);
        }
    }
}
