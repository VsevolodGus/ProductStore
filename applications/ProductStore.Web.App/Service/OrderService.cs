using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using System;
using System.Linq;
using System.Collections.Generic;
using Store;
using Store.Messages;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProductStore.Web.App
{
    public class OrderService
    {
        private readonly IProductRepository productRepository;
        private readonly IMakerRepository makerRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        protected ISession Session => httpContextAccessor.HttpContext.Session;

        public OrderService(IProductRepository productRepository,
                            IMakerRepository makerRepository,
                            IOrderRepository orderRepository,
                            INotificationService notificationService,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.productRepository = productRepository;
            this.makerRepository = makerRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        

        public async Task<(bool hasValue, OrderModel model)> TryGetModelAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
                return (true, await MapAsync(order));

            return (false, null);
        }

        internal async Task<(bool hasValue, Order order)> TryGetOrderAsync()
        {
            if (Session.TryGetCart(out Cart cart))
            {
                var order = await orderRepository.GetOrderFromCashAsync();

                return (true, order);
            }

            return (false, null);
        }

        internal async Task<OrderModel> MapAsync(Order order)
        {
            var products = await GetProductsAsync(order);
            var items = from item in order.Items
                        join product in products on item.ProductId equals product.Id
                        select new OrderItemModel
                        {
                            Id = product.Id,
                            Title = product.Title,
                            Count = item.Count,
                            MakerId = product.IdMaker,
                            MakerTitle = makerRepository.GetById(product.IdMaker).Title,
                            Price = product.Price,
                        };

            return new OrderModel
            {
                Id = order.Id,
                Items = items.ToList(),
                CellPhone = order.CellPhone,
                TotalCount = order.TotalCount,
                TotalPrice = order.TotalPrice,
                DeliveryDescription = order.Delivery?.Description,
                DeliveryPrice = order.Delivery?.PriceDelivery,
                PaymentDescription = order.Payment?.Description
            };

        }

        internal async Task<IEnumerable<Product>> GetProductsAsync(Order order)
        {
            var bookIds = order.Items.Select(item => item.ProductId);

            return await productRepository.GetAllByIdsAsync(bookIds);
        }

        public async Task<OrderModel> AddProductAsync(int productId, int count)
        {
            if (count < 1)
                throw new InvalidOperationException("");

            var (hasValue, order) = await TryGetOrderAsync();
            if (!hasValue)
                order = await orderRepository.CreateAsync();

            await AddOrUpdateProductAsync(order, productId, count);
            UpdateSession(order);

            return await MapAsync(order);
        }

        public async Task AddOrUpdateProductAsync(Order order, int productId,int count)
        {
            var product = await productRepository.GetByIdAsync(productId);

            if (order.Items.TryGet(productId, out OrderItem orderItem))
                orderItem.Count += count;
            else
                order.Items.Add(product.Id, product.Price, count);

            await orderRepository.UpdateAsync(order);
        }

        void UpdateSession(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        public async Task<Order> GetOrderAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();

            if (hasValue)
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        public async Task<OrderModel> UpdateProdurctAsync(int prodcutId, int count)
        {
            var order = await GetOrderAsync();
            order.Items.Get(prodcutId).Count = count;

            await orderRepository.UpdateAsync(order);
            UpdateSession(order);

            return await MapAsync(order);
        }

        public async Task<OrderModel>RemoveFullProductAsync(int productId) 
        {
            var order = await GetOrderAsync();
            order.Items.RemoveProduct(productId);

            await orderRepository .UpdateAsync(order);
            UpdateSession(order);

            return await MapAsync (order);
        }

        public async Task<OrderModel> RemoveItemAsync(int productId)
        {
            var order = await GetOrderAsync();
            order.Items.RemoveItem(productId);

            await orderRepository .UpdateAsync(order);
            UpdateSession(order);

            return await MapAsync(order);
        }

        

        public async Task<OrderModel> SendConfirmationAsync(string cellPhone,string email)
        {
            var order = await GetOrderAsync();
            

            var model = await MapAsync(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 2002; // здесь должен быть генератор кодов
                model.CellPhone = formattedPhone;
                Session.SetInt32(formattedPhone, confirmationCode);
                notificationService.SendConfirmationCode(formattedPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";

            if (email == null || !Order.TryFormatEmail(email))
                model.Errors["email"] = "Почта не соответствует формату  Brian@example.com";
            
            order.Email = email;
            UpdateSession(order);
            await orderRepository.UpdateAsync(order);

            return model;
        }

        public async Task<OrderModel> AgainSendConfirmationAsync(string cellPhone)
        {
            var order = await GetOrderAsync();

            var model = await MapAsync(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 2002; // здесь должен быть генератор кодов
                model.CellPhone = formattedPhone;
                Session.SetInt32(formattedPhone, confirmationCode);
                notificationService.SendConfirmationCode(formattedPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";

            return model;
        }
        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        private bool TryFormatPhone(string cellPhone, out string formattedPhone)
        {
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(cellPhone, "ru");
                formattedPhone = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
                return true;
            }
            catch (NumberParseException)
            {
                formattedPhone = null;
                return false;
            }
        }

        public async Task<OrderModel> ConfirmCellPhoneAsync(string cellPhone, int confirmationCode)
        {
            int? storeCode = Session.GetInt32(cellPhone);

            var model = new OrderModel
            {
                CellPhone = cellPhone
            };

            if (storeCode == null)
            {
                model.Errors["code"] = "Что-то случилось. Попробуйте получить код ещё раз.";

                return model;
            }    

            if(storeCode != confirmationCode)
            {
                model.Errors["code"] = "Невернвй код, проверьте код и попробуйте еще раз";

                return model;
            }

            var order = await GetOrderAsync();
            order.CellPhone = cellPhone;
            await orderRepository.UpdateAsync(order);

            Session.Remove(cellPhone);

            return await MapAsync(order);
        }

        public async Task<OrderModel> SetDeliveryAsync(OrderDelivery delivery)
        {
            var order = await GetOrderAsync();
            order.Delivery = delivery;
            
            await orderRepository.UpdateAsync(order);

            return await MapAsync(order);
        }

        public async Task<OrderModel>SetPaymentAsync(OrderPayment payment)
        {
            var order = await GetOrderAsync();
            order.Payment = payment;
            
            await orderRepository.UpdateAsync(order);

            await Finish();
           
            return await MapAsync(order);
        }

        private async Task Finish()
        {
            var order = await GetOrderAsync();

            await orderRepository.SendFileAsync(Order.Mapper.Map(order));            
            Session.RemoveCart();
            notificationService.StrtProcces(order);
        }
    }
}
