﻿using Store.Entities;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Store
{
    public class Order
    {
        private readonly OrderEntity dto;
        public Guid ID => dto.ID;

        public string CellPhone
        {
            get => dto.CellPhone;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(nameof(value)+"CellPhone for Order");

                dto.CellPhone = value;
            }
        }

        public string Email
        {
            get => dto.Email;
            set
            {
                if (value == null || !TryFormatEmail(value))
                    throw new ArgumentException("no correct value for Email");
                
                dto.Email = value;
            }
        }

        public static bool TryFormatEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9]+\@[a-z]{2,15}\.[a-z]{2,5}$");
        }

        public OrderItemCollection Items { get; set; }

        public int TotalCount => Items.Sum(item => item.Count);

        public decimal TotalPrice => Items.Sum(item => item.Count * item.Price) + 
                                                            (Delivery?.PriceDelivery ?? 0m);
        public OrderDelivery Delivery 
        { 
            get
            {
                if (dto.DeliveryUniqueCode == null)
                    return null;

                return new OrderDelivery(dto.DeliveryUniqueCode, 
                                         dto.DeliveryDescription,
                                         dto.DeliveryPrice, 
                                         dto.DeliveryParameters);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value) + "for SET Delivery Order");

                dto.DeliveryUniqueCode = value.UniqueCode;
                dto.DeliveryDescription = value.Description;
                dto.DeliveryPrice = value.PriceDelivery;
                dto.DeliveryParameters = value.Parametrs.ToDictionary(p=> p.Key, p=> p.Value);
            }
        }
        
        public OrderPayment Payment 
        { 
            get
            {
                if (dto.PaymentUniqueCode == null)
                    return null;

                return new OrderPayment(dto.PaymentUniqueCode, 
                                        dto.PaymentDescription,
                                        dto.PaymentParameters);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value) + "for SET Payment Order");

                dto.PaymentUniqueCode = value.UniqueCode;
                dto.PaymentDescription = value.Description;
                dto.PaymentParameters = value.Parametrs.ToDictionary(p=> p.Key, p=> p.Value);
            }
        }
        
        public Order(OrderEntity dto)
        {
            this.dto = dto;

            Items = new OrderItemCollection(this.dto);
        }

        public static class DtoFactory
        {
            public static OrderEntity Create() => new OrderEntity();
        }


        public static class Mapper
        {
            public static Order Map(OrderEntity dto) => new Order(dto);

            public static OrderEntity Map(Order domain) => domain.dto;
        }
    }
}
