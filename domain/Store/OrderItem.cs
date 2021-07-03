using Store.Data;
using System;

namespace Store
{
    public class OrderItem
    {
        private readonly OrderItemDto dto;

        public int ProductId => dto.ProductId;

        public int Count
        {
            get => dto.Count;
            set
            {
                ThrowExceptionForNoCorrectCount(value);

                dto.Count = value;
            }
        }

        public decimal Price
        {
            get => dto.Price;
            set => dto.Price = value;
        }

        public OrderItem(OrderItemDto dto)
        {
            this.dto = dto;
        }

        private static void ThrowExceptionForNoCorrectCount(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("no correct value count");
        }

        public static class DtoFactory
        {
            public static OrderItemDto Create(OrderDto order, int productId, int count, decimal price)
            {
                ThrowExceptionForNoCorrectCount(count);

                if (order == null)
                    throw new ArgumentNullException("is order null");

                return new OrderItemDto
                {
                    ProductId = productId,
                    Count = count,
                    Price = price,
                    Order = order
                };
            }
        }

        public static class Mapper
        {
            public static OrderItem Map(OrderItemDto dto) => new OrderItem(dto);

            public static OrderItemDto Map(OrderItem orderItem) => orderItem.dto;
        }
    }
}
