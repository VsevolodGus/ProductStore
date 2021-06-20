using System;
using System.Collections;
using System.Collections.Generic;

namespace Store
{
    public class OrderItemCollection : IReadOnlyCollection<OrderItem>
    {
        private readonly List<OrderItem> items;

        public OrderItemCollection(IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            this.items = new List<OrderItem>(items);
        }

        public int Count => items.Count;

        public IEnumerator<OrderItem> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (items as IEnumerable).GetEnumerator();
        }

        public bool TryGet(int productId, out OrderItem orderItem)
        {
            var index = items.FindIndex(item => item.ProductId == productId);
            if (index == -1)
            {
                orderItem = null;
                return false;
            }

            orderItem = items[index];
            return true;
        }

        public OrderItem Get(int productId)
        {
            if (TryGet(productId, out OrderItem orderItem))
                return orderItem;

            throw new InvalidOperationException("Product not found");
        }

        public OrderItem Add(int productId, decimal price, int count)
        {
            if (TryGet(productId, out OrderItem orderItem))
                throw new InvalidOperationException("Product already exists");

            orderItem = new OrderItem(productId, count, price);
            items.Add(orderItem);

            return orderItem;
        }

        // удаление целого пункта заказа
        public void RemoveProduct(int productId)
        {
            var index = items.FindIndex(item => item.ProductId == productId);
            if (index == -1)
                throw new InvalidOperationException("Can't find book to remove from order.");

            items.RemoveAt(index);
        }

        // удаление одного экзампляра из пункта
        public void RemoveItem(int productId)
        {
            var index = items.FindIndex(item => item.ProductId == productId);
            if (index == -1)
                throw new InvalidOperationException("Can't find book to remove from order.");

            if (items[index].Count == 1)
                items.RemoveAt(index);
            else
                items[index].Count -= 1;
        }
    }
}
