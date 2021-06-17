using ProductStore.Web.Contract;
using Store.Contract;
using System.Collections.Generic;

namespace Store.SberKassa
{
    public class SberKassaPaymentService : IPaymentService, IWebContractorService
    {
        public string UniqueCode => "SberKassa";

        public string Title => "Оплата банковской картой";

        public string GetUri => "/SberKassa/";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(UniqueCode, "Оплатой картой", new Dictionary<string, string>());
        }

        public Form MoveNextForm(int orderId, int step)
        {
            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}