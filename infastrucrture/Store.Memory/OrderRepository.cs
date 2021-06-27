using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Store.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextFactory dbContextFactory;
        //private readonly string path = $"{Environment.CurrentDirectory}\\OrderReposytory.json";

        public OrderRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        public Order Create()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = Order.DtoFactory.Create();
            dbContext.Orders.Add(dto);
            dbContext.SaveChanges();

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
            return Order.Mapper.Map(dto);
        }

        public Order GetById(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = dbContext.Orders
                               .Include(order => order.Items)
                               .Single(order => order.Id == id);

            return Order.Mapper.Map(dto);
        }

        public void Update(Order order)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));
            //using (StreamWriter writer = File.CreateText(path))
            //{
            //    string output = JsonConvert.SerializeObject(orders);
            //    writer.Write(output);
            //}

            dbContext.SaveChanges();
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



