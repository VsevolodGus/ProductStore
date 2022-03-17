using Store.Data;
using System.Threading.Tasks;

namespace Store
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync();

        Task<Order> GetOrderFromCashAsync();

        Task UpdateAsync(Order order);

        Task SendFileAsync(OrderDto dto);
    }
}
