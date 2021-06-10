using System;
using System.Collections.Generic;

namespace Store
{
    public class OrderDelivery
    {
        public string UniqueCode { get; }

        public string Description { get; }

        public decimal Amount { get; }

        public IReadOnlyDictionary<string,string> Parametrs { get; }

        public OrderDelivery(string uniqueCode, string description, decimal amount,
                                IReadOnlyDictionary<string,string> parametrs)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
                throw new ArgumentException(nameof(uniqueCode));
            if (parametrs == null)
                throw new ArgumentException(nameof(parametrs));

            UniqueCode = uniqueCode;
            Description = description;
            Amount = amount;
            Parametrs = parametrs;
        }
    }
}
