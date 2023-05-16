using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using Store;
using Store.Data;
using Store.IntarfaceRepositroy;
using Store.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App
{
    public class OrderService
    {
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReadonlyRepository<ProductEntity> _readonlyRepository;
        private readonly IReadonlyRepository<MakerEntity> _makers;
        private readonly IRepository<OrderEntity> _orders;
        private readonly IUnitOfWork _unitOfWork;
        protected ISession Session => httpContextAccessor.HttpContext.Session;

        public OrderService(
                            INotificationService notificationService,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        
        /// <summary>
        /// Попытка получение модели заказа
        /// </summary>
        /// <returns>инициализировано или нет, модель заказа</returns>
        public async Task<(bool hasValue, OrderModel model)> TryGetModelAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();
            if (hasValue)
                return (true, await MapAsync(order));

            return (false, null);
        }

        /// <summary>
        /// Получение сущности заказа
        /// </summary>
        /// <returns>инициализировано или нет, сущность заказа</returns>
        internal async Task<(bool hasValue, Order order)> TryGetOrderAsync(CancellationToken cancellationToken = default)
        {
            if (Session.TryGetCart(out Cart cart))
            {
                var order = await _orders.FirstOrDefaultAsync(c=> c.Id == cart.OrderId, cancellationToken);

                return (true, Order.Mapper.Map(order));
            }

            return (false, null);
        }

        /// <summary>
        /// Маппинг сущности в модель
        /// </summary>
        /// <param name="order">сущность</param>
        /// <returns>модель</returns>
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
                            MakerId = product.MakerID,
                            //TODO избавить от await blabla().Result
                            MakerTitle = _makers.FirstOrDefaultAsync(c => c.ID == product.MakerID).Result.Title,
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


        /// <summary>
        /// Получение продуктов по заказу
        /// </summary>
        /// <param name="order">заказ</param>
        /// <returns>список продуктов</returns>
        internal async Task<IEnumerable<Product>> GetProductsAsync(Order order, CancellationToken cancellationToken = default)
        {
            var bookIds = order.Items.Select(item => item.ProductId);
            var products = await _readonlyRepository.ToArrayAsync(c=> bookIds.Contains(c.Id), cancellationToken);
            return products.Select(Product.Mapper.Map);
        }

        /// <summary>
        /// Добавление позиции в заказ
        /// </summary>
        /// <param name="productId">идентификатор продукта</param>
        /// <param name="count">кол-во</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> AddProductAsync(int productId, int count)
        {
            if (count < 1)
                throw new InvalidOperationException("");

            var (hasValue, order) = await TryGetOrderAsync();
            
            if (!hasValue)
                order = Order.Mapper.Map(Order.DtoFactory.Create());

            await AddOrUpdateProductAsync(order, productId, count);
            UpdateSession(order);

            return await MapAsync(order);
        }

        /// <summary>
        /// Добавление или обновление заказа
        /// </summary>
        /// <param name="order">заказ</param>
        /// <param name="productId">идентификатор продукта</param>
        /// <param name="count">кол-во добавляемой позиции</param>
        public async Task AddOrUpdateProductAsync(Order order, int productId,int count, CancellationToken cancellationToken = default)
        {
            if (order.Items.TryGet(productId, out OrderItem orderItem))
                orderItem.Count += count;
            else
            {
                var product = await _readonlyRepository.FirstOrDefaultAsync(c => c.Id == productId, cancellationToken);
                order.Items.Add(product.Id, product.Price, count);
            }

            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);
        }

        /// <summary>
        /// Обновление заказа в сессии
        /// </summary>
        /// <param name="order">заказ</param>
        void UpdateSession(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <returns>сущность заказа</returns>
        public async Task<Order> GetOrderAsync()
        {
            var (hasValue, order) = await TryGetOrderAsync();

            if (hasValue)
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        /// <summary>
        /// Обновление позиций в заказе
        /// </summary>
        /// <param name="prodcutId">идентификатор позиции</param>
        /// <param name="count">кол-во позиции</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> UpdateProdurctAsync(int prodcutId, int count, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync();
            order.Items.Get(prodcutId).Count = count;

            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            UpdateSession(order);

            return await MapAsync(order);
        }

        /// <summary>
        /// Удаление позиции из заказа
        /// </summary>
        /// <param name="productId">идентификатор позиции</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel>RemoveFullProductAsync(int productId, CancellationToken cancellationToken = default) 
        {
            var order = await GetOrderAsync();
            order.Items.RemoveProduct(productId);

            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            UpdateSession(order);

            return await MapAsync (order);
        }

        /// <summary>
        /// Удаление элемента из позиции заказа
        /// </summary>
        /// <param name="productId">идентификатор позиции</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> RemoveItemAsync(int productId, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync();
            order.Items.RemoveItem(productId);

            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);
            UpdateSession(order);

            return await MapAsync(order);
        }

        /// <summary>
        /// Отправка кода подтверждения
        /// </summary>
        /// <param name="cellPhone">номер телефона который подтверждают</param>
        /// <param name="email">почта которую подтверждают</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> SendConfirmationAsync(string cellPhone,string email, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync();
            

            var model = await MapAsync(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 1111; // здесь должен быть генератор кодов
                model.CellPhone = formattedPhone;
                Session.SetInt32(formattedPhone, confirmationCode);
                notificationService.SendConfirmationCodeToPhone(formattedPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";

            if (email == null || !Order.TryFormatEmail(email))
                model.Errors["email"] = "Почта не соответствует формату  Brian@example.com";
            
            order.Email = email;
            UpdateSession(order);
            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return model;
        }

        /// <summary>
        /// Повторная подтверждение телефона
        /// </summary>
        /// <param name="cellPhone">номер телефона</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> AgainSendConfirmationAsync(string cellPhone)
        {
            var order = await GetOrderAsync();

            var model = await MapAsync(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 2002; // здесь должен быть генератор кодов
                model.CellPhone = formattedPhone;
                Session.SetInt32(formattedPhone, confirmationCode);
                notificationService.SendConfirmationCodeToPhone(formattedPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";

            return model;
        }
        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        /// <summary>
        /// Проверка формата телефона
        /// </summary>
        /// <param name="cellPhone"></param>
        /// <param name="formattedPhone"></param>
        /// <returns>подходит или нет</returns>
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

        /// <summary>
        /// Проверка кода подтверждения  телефона
        /// </summary>
        /// <param name="cellPhone">номер телефона который подтверждают</param>
        /// <param name="confirmationCode">код подтверждения</param>
        /// <returns>модель заказа</returns>
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
                model.Errors["code"] = "Невернывй код, проверьте код и попробуйте еще раз";

                return model;
            }

            var order = await GetOrderAsync();
            order.CellPhone = cellPhone;
            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync();

            Session.Remove(cellPhone);

            return await MapAsync(order);
        }

        /// <summary>
        /// Установка доставки в заказе
        /// </summary>
        /// <param name="delivery">модель доставки заказа</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel> SetDeliveryAsync(OrderDelivery delivery, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync();
            order.Delivery = delivery;
            
            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return await MapAsync(order);
        }

        /// <summary>
        /// Установка оплаты заказа
        /// </summary>
        /// <param name="payment">модель оплаты заказа</param>
        /// <returns>модель заказа</returns>
        public async Task<OrderModel>SetPaymentAsync(OrderPayment payment, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync();
            order.Payment = payment;

            _orders.Update(Order.Mapper.Map(order));
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await Finish();
           
            return await MapAsync(order);
        }

        /// <summary>
        /// Конец оформления заказа
        /// </summary>
        private async Task Finish()
        {
            var order = await GetOrderAsync();

            await _unitOfWork.SaveChangeAsync();
            Session.RemoveCart();
            notificationService.SendOrderNotification(order);
        }
    }
}
