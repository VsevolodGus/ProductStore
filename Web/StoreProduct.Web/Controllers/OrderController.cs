using Microsoft.AspNetCore.Mvc;
using ProductStore.Web.App.Service;
using ProductStore.Web.Contract;
using Store.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProduct.Web.Controllers
{
    //TODO решить проблему с роутами, ибо сейчас все привязано к названию метода, так не должно быть!
    public class OrderController : Controller
    {
        private readonly IEnumerable<IDeliveryService> deliveryServices;
        private readonly IEnumerable<IPaymentService> paymentServices;
        private readonly IEnumerable<IWebContractorService> webContractServices;
        private readonly IOrderService orderService;

        public OrderController(IEnumerable<IDeliveryService> deliveryServices,
                               IEnumerable<IPaymentService> paymentServices,
                               IEnumerable<IWebContractorService> webContractServices,
                               IOrderService orderService)
        {
            this.deliveryServices = deliveryServices;
            this.paymentServices = paymentServices;
            this.webContractServices = webContractServices;
            this.orderService = orderService;
        }

        /// <summary>
        /// Получение страницы заказа
        /// </summary>
        /// <returns>страница заказа</returns>

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var (hasValue, model) = await orderService.TryGetModelAsync(HttpContext.RequestAborted);
            if (hasValue)
                return View(model);

            return View("Empty");
        }

        /// <summary>
        /// Добавление элемента в заказ
        /// </summary>
        /// <param name="id">идентификатор добавляемого продукта продукта</param>
        /// <returns>страница информации</returns>
        [HttpPost]
        public async Task<IActionResult> AddItem(int id)
        {
            await orderService.AddProductAsync(id, 1);

            return RedirectToAction("InfoProduct", "Info", new { id = id });
        }

        /// <summary>
        /// Удаление элемента из заказа
        /// </summary>
        /// <param name="id">идентификатор элемента</param>
        /// <returns>страница просмотра заказа</returns>
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            var model = await orderService.RemoveItemAsync(id);

            return View("Index", model);
        }

        /// <summary>
        /// Удаление позиции из заказа
        /// </summary>
        /// <param name="id">идентификатор продукта для удаления из заказа</param>
        /// <returns>страница просмотра заказа</returns>
        [HttpPost]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var model = await orderService.RemoveFullProductAsync(id);

            return View("Index", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellPhone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SendConfirmation(string cellPhone, string email)
        {
            var model = await orderService.SendConfirmationAsync(cellPhone,email);

            if (model.Errors.Count > 0)
                return View("Index", model);

            return View("Confirmation", model);
        }
        
        /// <summary>
        /// Повторное отправка кода
        /// </summary>
        /// <param name="cellPhone">номер телефона</param>
        /// <returns>страница подтверждения</returns>

        [HttpPost]
        public async Task<IActionResult> AgainSendConfimation(string cellPhone)
        {
            var model = await orderService.AgainSendConfirmationAsync(cellPhone);

            return View("Confirmation", model);
        }

        /// <summary>
        /// Подтверждение номера телефона
        /// </summary>
        /// <param name="cellPhone">номер телефона</param>
        /// <param name="code">код</param>
        /// <returns>следующий этап заказа, или страница повторного ввода кода</returns>
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

        /// <summary>
        /// Получение uri по методу 
        /// </summary>
        /// <param name="action">имя метода</param>
        /// <returns>uri метода</returns>
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

        /// <summary>
        /// оформление заказа, по этапам, выбор места доставки заказа в одну из предложенных точек 
        /// </summary>
        /// <param name="serviceName">имя сервиса</param>
        /// <returns>начальная страница оформления доставки</returns>

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

        /// <summary>
        /// Поэтапное оформление доставки заказа
        /// </summary>
        /// <param name="serviceName">имя сервиса</param>
        /// <param name="step">номер шага</param>
        /// <param name="values">родительские значения</param>
        /// <returns>страница оформления доставки</returns>
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

        /// <summary>
        /// начало выбора способа оплаты заказа
        /// </summary>
        /// <param name="serviceName">имя сервиса</param>
        /// <returns>начальная страница оформления оплаты заказа</returns>
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
        
        /// <summary>
        /// Поэтапное оформление оплаты заказа
        /// </summary>
        /// <param name="serviceName">имя сервиса</param>
        /// <param name="step">номер шага</param>
        /// <param name="values">родительские значения</param>
        /// <returns>страница оформления оплаты</returns>
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


