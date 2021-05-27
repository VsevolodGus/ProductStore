using System.Collections.Generic;
using System.Linq;
using System;

namespace Store
{
    public class Order
    {
        private readonly List<OrderItem> items;

        public int Id { get; set; }
        public int TotalCount => items.Sum(item => item.Count);

        public decimal TotalPrice => items.Sum(item => item.Count * item.Price);

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException("no correct source OrderItem for Order");

            Id = id;

            this.items = items.ToList();
        }

        public IReadOnlyCollection<OrderItem> Items
        {
            get { return items; }
        }

        public OrderItem GetItemById(int id)
        {
            int index = items.FindIndex(item => item.ProductId == id);

            if (index == -1)
                throw new ArgumentException($"Not found OrderItrem with {id} in the Order");
            
            return items[index];
        }

        public void RemoveOrderItem(int id)
        {
            int index = items.FindIndex(item => item.ProductId == id);

            if (index == -1)
                throw new ArgumentException($"Not found OrderItrem with {id} in the Order");

            if (items[index].Count > 1)
                items[index].Count -= 1;
            else 
                items.RemoveAt(index);
        }

        public void RemoveFullOrderItem(int id)
        {
            int index = items.FindIndex(item => item.ProductId == id);

            if (index == -1)
                throw new ArgumentException($"Not found OrderItrem with {id} in the Order");

            items.RemoveAt(index);
        }

        public void AddOrUpdate(Product product, int count)
        {
            if (product == null)
                throw new ArgumentNullException("when adding order, passing null product");
            else if(count <= 0)
                throw new ArgumentNullException("when adding order, passing no correct count");


            int index = items.FindIndex(item => item.ProductId == product.Id);


            if (index == -1)
                items.Add(new OrderItem(product.Id, count, product.Price));
            else
                items[index].Count += count;
        }
    }
}
