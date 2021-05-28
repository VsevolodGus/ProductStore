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


        private OrderModel Map(Order order)
        {
            var productIds = order.Items.Select(item => item.ProductId);
            var products = productRepository.GetAllByIds(productIds);
            var itemModels = from item in order.Items
                             join product in products on item.ProductId equals product.Id
                             select new OrderItemModel
                             {
                                 Id = product.Id,
                                 Title = product.Title,
                                 Manufacturer = product.Manufacture,
                                 Count = item.Count,
                                 Price = item.Price,
                             };

            return new OrderModel
            {
                Id = order.Id,
                Items = itemModels.ToList(),
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetCart(out Cart cart))
            {
                var order = orderRepository.GetById(cart.OrderId);
                OrderModel model = Map(order);

                return View(model);
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
