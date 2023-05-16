using Store;
using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Web.App.Service;

public interface IOrderService
{
    Task<(bool hasValue, OrderModel model)> TryGetModelAsync(CancellationToken cancellationToken);
    Task<OrderModel> AddProductAsync(int productId, int count);
    Task AddOrUpdateProductAsync(Order order, int productId, int count, CancellationToken cancellationToken = default);
    Task<Order> GetOrderAsync();
    Task<OrderModel> UpdateProdurctAsync(int prodcutId, int count, CancellationToken cancellationToken = default);
    Task<OrderModel> RemoveFullProductAsync(int productId, CancellationToken cancellationToken = default);
    Task<OrderModel> RemoveItemAsync(int productId, CancellationToken cancellationToken = default);
    Task<OrderModel> SendConfirmationAsync(string cellPhone, string email, CancellationToken cancellationToken = default);
    Task<OrderModel> AgainSendConfirmationAsync(string cellPhone);
    Task<OrderModel> ConfirmCellPhoneAsync(string cellPhone, int confirmationCode);
    Task<OrderModel> SetDeliveryAsync(OrderDelivery delivery, CancellationToken cancellationToken = default);
    Task<OrderModel> SetPaymentAsync(OrderPayment payment, CancellationToken cancellationToken = default);
}
