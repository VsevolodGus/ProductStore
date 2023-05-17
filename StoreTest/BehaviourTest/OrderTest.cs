using Store;
using Store.Entities;
using System;
using Xunit;

namespace StoreTest
{
    public class OrderTest
    {
        private static Order CreateEmptyTestOrder()
        {
            return new Order(new OrderEntity
            {
                ID = Guid.NewGuid(),
                Items = new OrderItemEntity[0]
            });
        }

        [Fact]
        public void TotalCount_WithEmptyCollections_ReturnsZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithEmptyCollections_ReturnsZero()
        {
            var order = CreateEmptyTestOrder();

            Assert.Equal(0, order.TotalPrice);
        }
        private static Order CreateTestOrder()
        {
            var id = Guid.NewGuid();
            return new Order(new OrderEntity
            {
                ID = Guid.NewGuid(),
                Items = new[]
                {
                    new OrderItemEntity { ProductID = 1, OrderID = id, Price = 10m, Count = 3},
                    new OrderItemEntity { ProductID = 2, OrderID = id, Price = 100m, Count = 5},
                }
            });
        }

        [Fact] 
        public void TotalCount_WithNonEmptyCollections_ReturnCalculateCount()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 + 5, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNonEmptyCollections_ReturnCalculatePrice()
        {
            var order = CreateTestOrder();

            Assert.Equal(3 * 10m  +  5 * 100m, order.TotalPrice);
        }
    }
}
