using System.Collections.Generic;
using Store;
using Store.Contract;
using ProductStore.Web.Contract;

namespace SberKassa
{
    public class SberKassaPaymentService : IWebContractorService, IPaymentService
    {
        public string UniqueCode => "SberKassa";

        public string Title => "Оплата банковской картой";

        public string GetUri => "/SberKassa/";
        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new List<Field>());
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Оплата картой", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step)
        {
            return new Form(UniqueCode, orderId, 2, true, new List<Field>());
        }
    }
}
