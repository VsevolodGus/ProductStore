using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Store.Contract;
using ProductStore.Web.Contract;
using ProductStore.Web.App;
using System;
using System.Threading.Tasks;


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
        public async Task<IActionResult> Index()
        {
            var (hasValue, model) = await orderService.TryGetModelAsync();
            if (hasValue)
                return View(model);

            return View("Empty");
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int id)
        {
            await orderService.AddProductAsync(id, 1);

            return RedirectToAction("InfoProduct", "Info", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var model = await orderService.RemoveItemAsync(id);

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var model = await orderService .RemoveFullProductAsync(id);

            return View("Index", model);
        }


        //////////////////////////////////////////////
        //////////////////////////////////////////////
        //////////////////////////////////////////////

        [HttpPost]
        public async Task<IActionResult> SendConfirmation(string cellPhone, string email)
        {
            var model = await orderService.SendConfirmationAsync(cellPhone,email);

            if (model.Errors.Count > 0)
                return View("Index", model);

            return View("Confirmation", model);
        }

        [HttpPost]
        public async Task<IActionResult> AgainSendConfimation(string cellPhone)
        {
            var model = await orderService.AgainSendConfirmationAsync(cellPhone);

            return View("Confirmation", model);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmCellPhone(string cellPhone, int code)
        {
            var model = await orderService.ConfirmCellPhoneAsync(cellPhone, code);

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
        public async Task<IActionResult> StartDelivery(string serviceName)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);
            var order = await orderService.GetOrderAsync();
            var form = deliveryService.FirstForm(order);

            var webContractorService = webContractServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorService == null)
                return View("DeliveryStep", form);

            var returnUri = GetReturnUri(nameof(NextDelivery));
            var redirectUri = webContractorService.StartSession(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> NextDelivery(string serviceName, int step, Dictionary<string, string> values)
        {
            var deliveryService = deliveryServices.Single(service => service.Name == serviceName);

            var form = deliveryService.NextForm(step, values);
            if (!form.IsFinal)
                return View("DeliveryStep", form);

            var delivery = deliveryService.GetDelivery(form);
            await orderService.SetDeliveryAsync(delivery);

            var paymentMethods = paymentServices.ToDictionary(service => service.Name,
                                                              service => service.Title);

            return View("PaymentMethod", paymentMethods);
        }

        // оформление оплаты 
        [HttpPost]
        public async Task<IActionResult> StartPayment(string serviceName)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);
            var order = await orderService.GetOrderAsync();
            var form = paymentService.FirstForm(order);

            var webContractorService = webContractServices.SingleOrDefault(service => service.Name == serviceName);
            if (webContractorService == null)
                return View("PaymentStep", form);

            var returnUri = GetReturnUri(nameof(NextPayment));
            var redirectUri = webContractorService.StartSession(form.Parameters, returnUri);

            return Redirect(redirectUri.ToString());
        }
        

        [HttpPost]
        public async Task<IActionResult> NextPayment(string serviceName, int step, Dictionary<string, string> values)
        {
            var paymentService = paymentServices.Single(service => service.Name == serviceName);

            var form = paymentService.NextForm(step, values);
            if (!form.IsFinal)
                return View("PaymentStep", form);

            var payment = paymentService.GetPayment(form);
            var model = await orderService.SetPaymentAsync(payment);

            return View("Finish", model);
        }
    }
}


