using Store.Data;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Store
{
    public class Order
    {
        private readonly OrderDto dto;
        public int Id => dto.Id;

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
                                        dto.PaymentParametrs);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value) + "for SET Payment Order");

                dto.PaymentUniqueCode = value.UniqueCode;
                dto.PaymentDescription = value.Description;
                dto.PaymentParametrs = value.Parametrs.ToDictionary(p=> p.Key, p=> p.Value);
            }
        }
        
        public Order(OrderDto dto)
        {
            this.dto = dto;

            Items = new OrderItemCollection(this.dto);
        }

        public static class DtoFactory
        {
            public static OrderDto Create() => new OrderDto();
        }


        public static class Mapper
        {
            public static Order Map(OrderDto dto) => new Order(dto);

            public static OrderDto Map(Order domain) => domain.dto;
        }
    }
}
