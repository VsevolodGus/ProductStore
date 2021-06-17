using System.Collections.Generic;

namespace Store.Contract
{
    public interface IPaymentService
    {
        string UniqueCode { get; }

        string Title { get; }

        Form CreateForm(Order order);

        Form MoveNextForm(int orderId, int step);

        OrderPayment GetPayment(Form form);
    }
}
