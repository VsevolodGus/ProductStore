using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

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
        public async  Task<Order> CreateAsync()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = Order.DtoFactory.Create();
            dbContext.Orders.Add(dto);
            await dbContext.SaveChangesAsync();

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

        public async Task<Order> GetByIdAsync(int id)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));

            var dto = await dbContext.Orders
                               .Include(order => order.Items)
                               .SingleAsync(order => order.Id == id);

            return Order.Mapper.Map(dto);
        }

        public async Task UpdateAsync(Order order)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));
            //using (StreamWriter writer = File.CreateText(path))
            //{
            //    string output = JsonConvert.SerializeObject(orders);
            //    writer.Write(output);
            //}

            await dbContext.SaveChangesAsync();
        }

        public async Task SendFile()
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));
            
            await dbContext.SaveChangesAsync();

            //using (StreamWriter writer = File.CreateText(path))
            //{
            //    writer.Dispose();
            //    writer.Write(string.Empty);
            //}
        }
    }
}



