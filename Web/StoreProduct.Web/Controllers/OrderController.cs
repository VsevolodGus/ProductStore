using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Store.Contract;
using ProductStore.Web.Contract;
using ProductStore.Web.App;
using System;
using StoreProduct.Web.Models;

namespace StoreProduct.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEnumerable<IDeliveryService> deliveryServices;
        private readonly IEnumerable<IPaymentService> paymentServices;
        private readonly IEnumerable<IWebContractorService> webContractServices;
        private readonly OrderService orderService;

        public OrderController(IEnumerable<IDeliveryService> deliveryServices,
                               IEnumerable<IPaymentService> paymentServices,
                               IEnumerable<IWebContractorService> webContractServices,
                               OrderService orderService)
        {
            this.deliveryServices = deliveryServices;
            this.paymentServices = paymentServices;
            this.webContractServices = webContractServices;
            this.orderService = orderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (orderService.TryGetModel(out OrderModel model))
                return View(model);

            return View("Empty");
        }

        [HttpPost]
        public IActionResult AddItem(int id)
        {
            orderService.AddProduct(id, 1);

            return RedirectToAction("InfoProduct", "Info", new { id = id });
        }

        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var model = orderService.RemoveItem(id);

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult RemoveProduct(int id)
        {
            var model = orderService.RemoveFullProduct(id);

            return View("Index", model);
        }


        //////////////////////////////////////////////
        //////////////////////////////////////////////
        //////////////////////////////////////////////

        [HttpPost]
        public IActionResult SendConfirmation(int id, string cellPhone)
        {
            var model = orderService.SendConfirmation(cellPhone);

            return View("Confirmation", model);
        }

        [HttpPost]
        public IActionResult ConfirmCellPhone(string cellPhone, int code)
        {
            var model = orderService.ConfirmCellPhone(cellPhone, code);

            if (model.Errors.Count > 0)
                return View("Confirmation", model);

            var deliveryMethods = deliveryServices.ToDictionary(service => service.Name,
                                                                service => service.Title);

            return View("DeliveryMethod", deliveryMethods);
        }


        //////////////////////////////////////////////
        //////////////////////////////////////////////
        //////////////////////////////////////////////

        private Uri GetReturnUri(string action)
        {
            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = Url.Action(action),
                Query = null,
            };

            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;

            return builder.Uri;
        }

        //офромление заказа, по этапам, выбор места доставки заказа в одну из предложенных точек 

        [HttpPost]
        public IActionResult StartDelivery(string serviceName)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);
            var order = orderService.GetOrder();
            var form = deliveryService.FirstForm(order);

            var webContractorService = webContractServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorService == null)
                return View("DeliveryStep", form);

            var returnUri = GetReturnUri(nameof(NextDelivery));
            var redirectUri = webContractorService.StartSession(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }

        [HttpPost]
        public IActionResult NextDelivery(string serviceName, int step, Dictionary<string, string> values)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);

            var form = deliveryService.NextForm(step, values);
            if (!form.IsFinal)
                return View("DeliveryStep", form);

            var delivery = deliveryService.GetDelivery(form);
            orderService.SetDelivery(delivery);

            var paymentMethods = paymentServices.ToDictionary(service => service.Name,
                                                              service => service.Title);

            return View("PaymentMethod", paymentMethods);
        }

        // оформление оплаты 
        [HttpPost]
        public IActionResult StartPayment(string serviceName)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);
            var order = orderService.GetOrder();
            var form = paymentService.FirstForm(order);

            var webContractorService = webContractServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorService == null)
                return View("PaymentStep", form);

            var returnUri = GetReturnUri(nameof(NextPayment));
            var redirectUri = webContractorService.StartSession(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }
        //{https://localhost:44305/YandexKassa/?orderId=2&returnUri=https%3A%2F%2Flocalhost%3A44305%2FOrder%2FNextPayment}
        //{https://localhost:44338/SberKassa/?orderId=1&returnUri=https%3A%2F%2Flocalhost%3A44338%2FOrder%2FNextPayment}

        [HttpPost]
        public IActionResult NextPayment(string serviceName, int step, Dictionary<string, string> values)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);

            var form = paymentService.NextForm(step, values);
            if (!form.IsFinal)
                return View("PaymentStep", form);

            var payment = paymentService.GetPayment(form);
            var model = orderService.SetPayment(payment);

            return View("Finish", model);
        }
    }
}


