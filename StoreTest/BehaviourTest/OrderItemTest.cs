using Store;
using Store.Data;
using System;
using Xunit;

namespace StoreTest
{
    public class OrderItemTest
    {
        [Fact]
        public void OrderItemDto_WithZeroCount_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = 0;
                OrderItem.DtoFactory.Create(new OrderDto(), 1, count, 10m);
            });
        }

        [Fact]
        public void OrderItemDto_WithNegative_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                int count = -1;
                OrderItem.DtoFactory.Create(new OrderDto(), 1, count, 10m);
            });
        }

        [Fact]
        public void OrderItemDto_WithPositive_SetCount()
        {
            int count = 10;
            var order = OrderItem.DtoFactory.Create(new OrderDto(), 1, count, 10m);

            Assert.Equal(10, order.Count);
        }

        [Fact]
        public void Count_WithZero_ThrowsArgumentOutOfRangeException()
        {
            var orderItemDto = OrderItem.DtoFactory.Create(new OrderDto(), 1, 10, 10m);
            var orderItem = new OrderItem(orderItemDto);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                orderItem.Count = 0;
            });
        }

        [Fact]
        public void Count_WithNegative_ThrowsArgumentOutOfRangeException()
        {
            var orderItemDto = OrderItem.DtoFactory.Create(new OrderDto(), 1, 10, 10m);
            var orderItem = new OrderItem(orderItemDto);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                orderItem.Count = -1;
            });
        }

        [Fact]
        public void Count_WithPositive_ThrowsArgumentOutOfRangeException()
        {
            var orderItemDto = OrderItem.DtoFactory.Create(new OrderDto(), 1, 10, 10m);
            var orderItem = new OrderItem(orderItemDto);

            orderItem.Count = 1000;

            Assert.Equal(1000, orderItem.Count);
        }


    }
}
