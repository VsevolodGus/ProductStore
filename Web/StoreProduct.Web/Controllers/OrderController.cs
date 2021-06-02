using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store;
using StoreProduct.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        public IActionResult AddItem(int id)
        {
            (Order order, Cart cart) = GetCreateOrderAndCart();

            var product = productRepository.GetAllById(id);

            order.AddOrUpdate(product, 1);

            SaveOrderAndCart(order,cart);

            return RedirectToAction("InfoProduct", "Info", product);
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            (Order order, Cart cart) = GetCreateOrderAndCart();

            if (order.GetItemById(id).Count <= 1)
                RemoveProduct(id);

            order.RemoveOrderItem(id);

            SaveOrderAndCart(order, cart);

            if (HttpContext.Session.TryGetCart(out Cart resultCart))
            {
                var resultOrder = orderRepository.GetById(resultCart.OrderId);
                OrderModel model = Map(resultOrder);

                return View("Index", model);
            }

            return View("Empty");
        }

        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            (Order order, Cart cart) = GetCreateOrderAndCart();

            order.RemoveFullOrderItem(id);

            SaveOrderAndCart(order, cart);

            if (HttpContext.Session.TryGetCart(out Cart resultCart))
            {
                var resultOrder = orderRepository.GetById(resultCart.OrderId);
                OrderModel model = Map(resultOrder);

                return View("Index",model);
            }

            return View("Empty");
        }

        //////////////////////////////////////////////
        //////////////////////////////////////////////
        ///
        
        private bool IsValideCellPhone(string cellPhone)
        {
            if (cellPhone == null)
                return false;

            return Regex.IsMatch(cellPhone, @"\+79\d{2}-\d{3}-\d{2}-\d{2}")
                    || Regex.IsMatch(cellPhone, @"89\d{2}-\d{3}-\d{2}-\d{2}")
                    || Regex.IsMatch(cellPhone, @"\+\d{11}")
                    || Regex.IsMatch(cellPhone, @"\d{11}");
        }

        public IActionResult SendConfirmation(int id, string cellPhone)
        {
            var order = orderRepository.GetById(id);
            var model = Map(order);

            if (!IsValideCellPhone(cellPhone))
            {
                model.Errors[cellPhone] = "Номер телефона должен соответсвовать формату +79876543210";
             
                return View("Index", model);
            }

            int code = 2002;
            HttpContext.Session.SetInt32(cellPhone,code);


            return View("Confirmation",
                        new ConfimationModel
                        {
                            OrderId = id,
                            CellPhone = cellPhone,
                        }) ;
        }
    }
}
