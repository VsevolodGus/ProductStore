using Newtonsoft.Json;
using Store.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Store.Memory
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextFactory dbContextFactory;
        private readonly string path = $"{Environment.CurrentDirectory}\\OrderCash.json";

        public OrderRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        // to do writting in cash brouser
        public async Task<Order> CreateAsync()
        { 
            var order = Order.DtoFactory.Create();

            if (!File.Exists(path))
            {
                File.CreateText(path).Dispose();
            }
            using (StreamReader reader = File.OpenText(path))
            {
                var fileText = await reader.ReadToEndAsync();
                var orderDto = JsonConvert.DeserializeObject<OrderDto>(fileText);

                if (orderDto != null)
                {
                    order = orderDto;
                    orderDto.DeliveryUniqueCode = null;
                    orderDto.DeliveryDescription = null;
                    orderDto.DeliveryParameters = null;
                    orderDto.DeliveryPrice = 0m;

                    orderDto.PaymentDescription = null;
                    orderDto.PaymentParametrs = null;
                    orderDto.PaymentUniqueCode = null;
                }
            }

            return Order.Mapper.Map(order);
        }

        public async Task<Order> GetOrderFromCashAsync()
        {
            var orderDto = Order.DtoFactory.Create();

            using (StreamReader reader = File.OpenText(path))
            {
                var fileText = await reader.ReadToEndAsync();
                orderDto = JsonConvert.DeserializeObject<OrderDto>(fileText);
                
                if (orderDto == null)
                    orderDto = Order.DtoFactory.Create();
                
            }

            return Order.Mapper.Map(orderDto);
        }

        public async Task UpdateAsync(Order order)
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                string output = JsonConvert.SerializeObject(Order.Mapper.Map(order), Formatting.None,
                                                new JsonSerializerSettings()
                                                {
                                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                });

                await writer.WriteAsync(output);
            }
        }

        public async Task SendFileAsync(OrderDto dto)
        {
            var dbContext = dbContextFactory.Create(typeof(OrderRepository));
            await dbContext.SaveChangesAsync();

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.Dispose();
                writer.Write(string.Empty);
            }
        }
    }
}



