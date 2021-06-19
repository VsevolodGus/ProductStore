using Microsoft.AspNetCore.Http;
using Store;
using Store.Messages;

namespace ProductStore.Web.App
{
    public class OrderService
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly INotificationService notificationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession Session => httpContextAccessor.HttpContext.Session;

        public OrderService(IProductRepository productRepository,
                            IOrderRepository orderRepository,
                            INotificationService notificationService,
                            IHttpContextAccessor httpContextAccessor)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.notificationService = notificationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        //public bool TryGetModel(out OrderModel model)
        //{
        //    if (TryGetOrder(out Order order))
        //    {
        //        model = Map(order);
        //        return true;
        //    }

        //    model = null;
        //    return false;
        //}

        internal bool TryGetOrder(out Order order)
        {
            if (Session.TryGetCart(out Cart cart))
            {
                order = orderRepository.GetById(cart.OrderId);
                return true;
            }

            order = null;
            return false;
        }

        //internal OrderModel Map(Order order)
        //{
        //    var books = GetBooks(order);
        //    var items = from item in order.Items
        //                join book in books on item.BookId equals book.Id
        //                select new OrderItemModel
        //                {
        //                    BookId = book.Id,
        //                    Title = book.Title,
        //                    Author = book.Author,
        //                    Price = item.Price,
        //                    Count = item.Count,
        //                };

        //    return new OrderModel
        //    {
        //        Id = order.Id,
        //        Items = items.ToArray(),
        //        TotalCount = order.TotalCount,
        //        TotalPrice = order.TotalPrice,
        //        CellPhone = order.CellPhone,
        //        DeliveryDescription = order.Delivery?.Description,
        //        PaymentDescription = order.Payment?.Description
        //    };
        //}
    }
}
