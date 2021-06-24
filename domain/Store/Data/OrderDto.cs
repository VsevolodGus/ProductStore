﻿using System.Collections.Generic;

namespace Store.Data
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string CellPhone { get; set; }

        public string DeliveryUniqueCode { get; set; }

        public string DeliveryDescription { get; set; }

        public string PaymentUniqueCode { get; set; }

        public string PaymentDescription { get; set; }

        public Dictionary<string, string> PaymentParametrs { get; set; }

        public IList<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}