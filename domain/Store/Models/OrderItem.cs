using Store.Data;
using System;

namespace Store
{
    public class OrderItem
    {
        private readonly OrderItemEntity dto;

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

        public OrderItem(OrderItemEntity dto)
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
            public static OrderItemEntity Create(OrderEntity order, int productId, int count, decimal price)
            {
                ThrowExceptionForNoCorrectCount(count);

                if (order == null)
                    throw new ArgumentNullException("is order null");

                return new OrderItemEntity
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
            public static OrderItem Map(OrderItemEntity dto) => new OrderItem(dto);

            public static OrderItemEntity Map(OrderItem orderItem) => orderItem.dto;
        }
    }
}
