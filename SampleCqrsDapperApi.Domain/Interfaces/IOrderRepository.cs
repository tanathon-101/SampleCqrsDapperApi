using SampleCqrsDapperApi.Domain.Entities;

namespace SampleCqrsDapperApi.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<bool> UpdateOrderCustomerAsync(int orderId, int newCustomerId);
    }
}