using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Store.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> orders = new List<Order>();
        private readonly string path = $"{Environment.CurrentDirectory}\\OrderReposytory.json";

        public OrderRepository()
        {
            //var fileExists = File.Exists(path);
            //if (!fileExists)
            //{
            //    File.CreateText(path).Dispose();
            //}

            //using (StreamReader reader = File.OpenText(path))
            //{
            //    var fileText = reader.ReadToEnd();
            //    orders = JsonConvert.DeserializeObject<List<Order>>(fileText);
            //    if (orders == null)
            //        orders = new List<Order>();
            //}
        }
        public Order Create()
        {
            int nextId = orders.Count + 1;
            //var order = new Order(nextId, new OrderItem[0]);

            //orders.Add(order);

            return null;
        }

        public Order GetById(int id)
        {
            return orders.Single(order => order.Id == id);
        }

        public void Update(Order order)
        {
            //using (StreamWriter writer = File.CreateText(path))
            //{
            //    string output = JsonConvert.SerializeObject(orders);
            //    writer.Write(output);
            //}
        }

        public void SendFile()
        {
            //using (StreamWriter writer = File.CreateText(path))
            //{
            //    writer.Dispose();
            //    writer.Write(string.Empty);
            //}
        }
    }
}