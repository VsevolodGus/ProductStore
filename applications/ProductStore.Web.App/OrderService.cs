using Microsoft.AspNetCore.Http;
using PhoneNumbers;
using System;
using System.Linq;
using System.Collections.Generic;
using Store;
using Store.Messages;


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

        
        public bool TryGetModel(out OrderModel orderModel)
        {
            if (TryGetOrder(out Order order))
            {
                orderModel = Map(order);
                return true;
            }

            orderModel = null; 
            return false;
        }
       
        public bool TryGetOrder(out Order order)
        {
            if(Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.GetById(cart.OrderId);
                return true;
            }

            order = null;
            return false;
        }
        internal OrderModel Map(Order order)
        {
            var products = GetProducts(order);
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
        internal IEnumerable<Product> GetProducts(Order order)
        {
            var bookIds = order.Items.Select(item => item.ProductId);

            return productRepository.GetAllByIds(bookIds);
        }

        public OrderModel AddProduct(int productId, int count)
        {
            if (count < 1)
                throw new InvalidOperationException("");

            if (!TryGetOrder(out Order order))
                order = orderRepository.Create();

            AddOrUpdateProduct(order, productId, count);
            UpdateSession(order);
            orderRepository.Update(order);

            return Map(order);
        }

        public void AddOrUpdateProduct(Order order, int productId,int count)
        {
            var product = productRepository.GetById(productId);

            if (order.Items.TryGet(productId, out OrderItem orderItem))
                orderItem.Count += count;
            else
                order.Items.Add(product.Id, product.Price, count);
        }

        void UpdateSession(Order order)
        {
            var cart = new Cart(order.Id, order.TotalCount, order.TotalPrice);
            Session.Set(cart);
        }

        public Order GetOrder()
        {
            if (TryGetOrder(out Order order))
                return order;

            throw new InvalidOperationException("Empty session.");
        }

        public OrderModel UpdateProduct(int prodcutId, int count)
        {
            var order = GetOrder();
            order.Items.Get(prodcutId).Count = count;

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public OrderModel RemoveFullProduct(int productId) 
        {
            var order = GetOrder();
            order.Items.RemoveProduct(productId);

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public OrderModel RemoveItem(int productId)
        {
            var order = GetOrder();
            order.Items.RemoveItem(productId);

            orderRepository.Update(order);
            UpdateSession(order);

            return Map(order);
        }

        public OrderModel SendConfirmation(string cellPhone)
        {
            var order = GetOrder();
            var model = Map(order);

            if (TryFormatPhone(cellPhone, out string formattedPhone))
            {
                var confirmationCode = 2002; // здесь должен быть генератор кодов
                model.CellPhone = formattedPhone;
                //model.CellPhone = cellPhone;
                Session.SetInt32(formattedPhone, confirmationCode);
                notificationService.SendConfirmationCode(formattedPhone, confirmationCode);
            }
            else
                model.Errors["cellPhone"] = "Номер телефона не соответствует формату +79876543210";

            return model;
        }

        private readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        internal bool TryFormatPhone(string cellPhone, out string formattedPhone)
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

        public OrderModel ConfirmCellPhone(string cellPhone, int confirmationCode)
        {
            int? storeCode = Session.GetInt32(cellPhone);
            var model = new OrderModel();

            if (storeCode == null)
            {
                model.Errors["cellPhone"] = "Что-то случилось. Попробуйте получить код ещё раз.";

                return model;
            }    

            if(storeCode != confirmationCode)
            {
                model.Errors["confirmationCode"] = "Невернвй код, проверьте код и попробуйте еще раз";

                return model;
            }

            var order = GetOrder();
            order.CellPhone = cellPhone;
            orderRepository.Update(order);

            Session.Remove(cellPhone);

            return Map(order);
        }

        public OrderModel SetDelivery(OrderDelivery delivery)
        {
            var order = GetOrder();
            order.Delivery = delivery;
            
            orderRepository.Update(order);

            return Map(order);
        }

        public OrderModel SetPayment(OrderPayment payment)
        {
            var order = GetOrder();
            order.Payment = payment;
            
            orderRepository.Update(order);
            Session.RemoveCart();

            return Map(order);
        }
    }
}
