using Store.Data;
using System.Threading.Tasks;

namespace Store
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <returns>созданный заказ</returns>
        Task<Order> CreateAsync();

        /// <summary>
        /// Получение заказа
        /// </summary>
        /// <returns>заказ</returns>
        Task<Order> GetOrderFromCashAsync();

        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <param name="order">обновленная сущность</param>
        Task UpdateAsync(Order order);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task SendFileAsync(OrderEntity dto);
    }
}
