using MediatR;
using SampleCqrsDapperApi.Application.DTOs;
using SampleCqrsDapperApi.Application.Queries;
using SampleCqrsDapperApi.Domain.Interfaces;

namespace SampleCqrsDapperApi.Application.Handlers;

public class GetCustomerWithOrdersQueryHandler : IRequestHandler<GetCustomerWithOrdersQuery, IEnumerable<CustomerWithOrdersDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerWithOrdersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerWithOrdersDto>> Handle(GetCustomerWithOrdersQuery request, CancellationToken cancellationToken)
    {
        var rawData = await _customerRepository.GetCustomersWithOrdersAsync();

        var customerDict = new Dictionary<int, CustomerWithOrdersDto>();

        foreach (var (customer, order) in rawData)
        {
            if (!customerDict.TryGetValue(customer.Id, out var customerDto))
            {
                customerDto = new CustomerWithOrdersDto
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    Orders = new List<OrderDto>()
                };
                customerDict.Add(customer.Id, customerDto);
            }

            if (order != null)
            {
                customerDto.Orders.Add(new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount
                });
            }
        }

        return customerDict.Values;
    }

}
