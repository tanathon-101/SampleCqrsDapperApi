using SampleCqrsDapperApi.Domain.Entities;

namespace SampleCqrsDapperApi.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<(Customer, Order)>> GetCustomersWithOrdersAsync();
    }
}