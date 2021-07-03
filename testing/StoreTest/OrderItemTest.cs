using Store;
using System;
using Xunit;

namespace StoreTest
{
    public class OrderItemTest
    {
        [Fact]
        public void OrderItem_WithZeroCount_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                int count = 0;
                new OrderItem(1, count, 0m);
            });
        }

        [Fact]
        public void OrderItem_WithNegativeCount_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                int count = -1;
                new OrderItem(1, count, 0m);
            });
        }

        [Fact]
        public void OrderItem_WithPositiveCount_Create()
        {
            int count = 1;

            var orderitem = new OrderItem(1, count, 0m);

            Assert.Equal(1, orderitem.ProductId);
            Assert.Equal(count, orderitem.Count);
            Assert.Equal(0m, orderitem.Price);
        }

        [Fact]
        public void SetCount_CountBecomeZero_ThrowArgumentException()
        {
            var item = new OrderItem(1, 5, 10m);

            int count = -5;

            Assert.Throws<ArgumentException>(() =>
            {
                item.Count += count;
            });
        }

        [Fact]
        public void SetCount_CountBecomeNegative_ThrowArgumentExeption()
        {
            var item = new OrderItem(1, 5, 10m);

            int count = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                item.Count += count;
            });
        }

        [Fact]
        public void SetCount_CountBecomePositive_ThrowArgumentException()
        {
            var item = new OrderItem(1, 5, 0m);

            int count = 5;

            item.Count += count;

            Assert.Equal(10, item.Count);
        }
    }
}
