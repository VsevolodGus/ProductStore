using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Store.Data;


namespace Store
{
    public class OrderItemCollection : IReadOnlyCollection<OrderItem>
    {
        private readonly OrderDto orderDto;
        private readonly List<OrderItem> items;

        public OrderItemCollection(OrderDto orderDto)
        {
            if (orderDto== null)
                throw new ArgumentNullException(nameof(orderDto));

            this.orderDto = orderDto;

            items = orderDto.Items
                                .Select(OrderItem.Mapper.Map)
                                .ToList();
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

        public OrderItem Add(int bookId, decimal price, int count)
        {
            if (TryGet(bookId, out OrderItem orderItem))
                throw new InvalidOperationException("Book already exists.");

            var orderItemDto = OrderItem.DtoFactory.Create(orderDto, bookId, count, price);
            orderDto.Items.Add(orderItemDto);

            orderItem = OrderItem.Mapper.Map(orderItemDto);
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
