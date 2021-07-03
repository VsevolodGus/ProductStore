using Store;
using System;
using System.Collections.Generic;
using Xunit;

namespace StoreTest
{
    public class OrderTest
    {
        [Fact]
        public void Order_CreateWithNull_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Order(1, null));
        }

        [Fact]
        public void Order_CreateWithCorrectSource_Create()
        {
            var order = new Order(1, new List<OrderItem>());

            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalCount_WithEmptyItems_ReturnZero()
        {
            var order = new Order(1, new List<OrderItem>());

            Assert.Equal(0, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithEmptyItems_ReturnZero()
        {
            var order = new Order(1, new List<OrderItem>());

            Assert.Equal(0, order.TotalPrice);
        }

        [Fact]
        public void TotalCount_WitNotEmptyItems_True()
        {
            var order = new Order(1, new[]
           {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,100m),
            });

            Assert.Equal(11, order.TotalCount);
        }

        [Fact]
        public void TotalPrice_WithNotEmptyItems_True()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,100m),
            });

            Assert.Equal(10 * 10m + 1 * 100m, order.TotalPrice);
        }


        [Fact]
        public void GetById_IdNoExists_ThrowAgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,1,0m),
                new OrderItem(2,1,0m),
            });

            int id = 0;

            Assert.Throws<ArgumentException>(() =>
            {
                order.GetItemById(id);
            });
        }

        [Fact]
        public void GetById_IdExists_ThrowAgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            int id = 1;
            var item = order.GetItemById(id);

            Assert.Equal(1, item.ProductId);
            Assert.Equal(10m, item.Count);
            Assert.Equal(10m, item.Price);

        }

        [Fact]
        public void RemoveOrderItem_NoExistOrderItem_ThrowArgumentException()
        {

            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            int id = 3;

            Assert.Throws<ArgumentException>(() =>
            {
                order.RemoveOrderItem(id);
            });
        }

        [Fact]
        public void RemoveOrderItem_ExistOrderItem_True()
        {

            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,2,0m),
            });

            int id = 2;

            order.RemoveOrderItem(id);

            Assert.Equal(1, order.GetItemById(id).Count);
        }
        [Fact]
        public void RemoveOrderItem_ExistOrderItemWitnOneCount_True()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            int id = 2;

            order.RemoveOrderItem(id);

            Assert.Equal(1, order.Items.Count);
        }

        [Fact]
        public void RemoveFullOrderItem_NoExistOrderItem_ThrowArgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            int id = 3;

            Assert.Throws<ArgumentException>(() =>
            {
                order.RemoveFullOrderItem(id);
            });
        }

        [Fact]
        public void RemoveFullOrderItem_ExistOrderItem_ThrowArgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            int id = 1;

            order.RemoveFullOrderItem(id);

            Assert.Equal(1, order.Items.Count);
        }

        [Fact]
        public void AddOrUpdate_WithProductIsNull_ThrowArgumentNullException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            Product product = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                order.AddOrUpdate(product, 10);
            });
        }

        [Fact]
        public void AddOrUpdate_WithZeroCount_ThrowArgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            Product product = new Product(1, "", 1, "", 0m, "");
            int count = 0;

            Assert.Throws<ArgumentException>(() =>
            {
                order.AddOrUpdate(product, count);
            });
        }

        [Fact]
        public void AddOrUpdate_WithNegativeCount_ThrowArgumentException()
        {
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            Product product = new Product(1, "", 1, "", 0m, "");
            int count = -10;

            Assert.Throws<ArgumentException>(() =>
            {
                order.AddOrUpdate(product, count);
            });
        }

        [Fact]
        public void AddOrUpdate_ExistsProduct_True()
        {
            int id = 1;
            var order = new Order(1, new[]
            {
                new OrderItem(id,10,10m),
                new OrderItem(2,1,0m),
            });

            Product product = new Product(id, "", 1, "", 0m, "");
            int count = 10;

            order.AddOrUpdate(product, count);

            Assert.Equal(20, order.GetItemById(id).Count);
        }

        [Fact]
        public void AddOrUpdate_NewProduct_True()
        {
            int id = 3;
            var order = new Order(1, new[]
            {
                new OrderItem(1,10,10m),
                new OrderItem(2,1,0m),
            });

            Product product = new Product(id, "", 1, "", 0m, "");
            int count = 10;

            order.AddOrUpdate(product, count);

            Assert.Equal(10, order.GetItemById(id).Count);
        }
    }
}
