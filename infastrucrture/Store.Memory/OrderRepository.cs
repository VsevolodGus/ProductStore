using System.Collections.Generic;
using System.Linq;

namespace Store.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>();

        public OrderRepository()
        {
            // if(JSON != null)
            // 
            // чтение JSON -> парсинг JSON -> созранение информации в orders
        }

        public Order Create()
        {
            int nextId = orders.Count + 1;
            var order = new Order(nextId, new OrderItem[0]);

            orders.Add(order);

            return order;
        }

        public Order GetById(int id)
        {
            return orders.Single(order => order.Id == id);
        }

        public void Update(Order order)
        {
            // здесь должна быть перезапись json файла с обновленнными данными
        }
    }
}
