using Store;
using Store.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace StoreTest
{
    public class OrderItemCollection
    {
        private static Order CreateTestOrder()
        {
            return new Order(new OrderDto
            {
                Id = 1,
                Items = new List<OrderItemDto>
                {
                    new OrderItemDto { ProductId = 1, Price = 10m, Count = 3},
                    new OrderItemDto { ProductId = 2, Price = 100m, Count = 5},
                }
            });
        }

        [Fact]
        public void Get_WithExistingItem_ReturnItems()
        {
            var order = CreateTestOrder();

            var orderItem = order.Items.Get(1);

            Assert.Equal(1, orderItem.ProductId);
            Assert.Equal(10m, orderItem.Price);
            Assert.Equal(3, orderItem.Count);
        }

        [Fact]
        public void GetWithNoExistingItem_ThrowsInvalidOperationsException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Get(10);
            });
        }

        [Fact]
        public void Add_WithExistiongItems_TrowsInvalidOperationException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Add(1, 10m, 100);
            });
        }

        [Fact]
        public void Add_WithNewItem()
        {
            var order = CreateTestOrder();

            order.Items.Add(3, 10m, 10);

            Assert.Equal(10, order.Items.Get(3).Count);
        }

        [Fact]
        public void RemoveItem_WithExistingItems_RemoveItems()
        {
            var order = CreateTestOrder();

            order.Items.RemoveItem(1);

            Assert.Equal(2, order.Items.Get(1).Count);
        }

        [Fact]
        public void RemoveItem_WithNonExistingItems_ThrowInvalidOperationException()
        {
            var order = CreateTestOrder();

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.RemoveItem(3);
            });
        }

        [Fact]
        public void RemoveProduct_WithExistingItems_RemoveProduct()
        {
            var order = CreateTestOrder();

            order.Items.RemoveProduct(1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.Get(1);
            });
        }

        [Fact]
        public void RemoveProduct_WithNonExisting_ThrowsInvalidOperationException()
        {
            var order = CreateTestOrder();


            Assert.Throws<InvalidOperationException>(() =>
            {
                order.Items.RemoveProduct(3);
            });
        }

    }   
}
