﻿using System.Collections.Generic;

namespace Store.Contract
{
    public interface IDeliveryService
    {
        string UniqueCode { get; }

        string Title { get; }

        Form CreateForm(Order order);

        Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values);

        OrderDelivery GetDelivery(Form form);

    }
}
