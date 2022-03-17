using System;
using System.Collections.Generic;

namespace Store
{
    public class OrderPayment
    {
        public string UniqueCode { get; }

        public string Description { get; }

        public IReadOnlyDictionary<string, string> Parametrs { get; }

        public OrderPayment(string uniquecode, string description, IReadOnlyDictionary<string,string> parametrs)
        {
            if (string.IsNullOrWhiteSpace(uniquecode))
                throw new ArgumentException(nameof(uniquecode));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(nameof(description));

            if (parametrs == null)
                throw new ArgumentNullException(nameof(parametrs));

            UniqueCode = uniquecode;
            Description = description;
            Parametrs = parametrs;
        }
    }
}
