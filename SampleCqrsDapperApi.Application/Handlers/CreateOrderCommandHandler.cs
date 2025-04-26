using MediatR;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;

namespace SampleCqrsDapperApi.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            OrderDate = request.OrderDate,
            TotalAmount = request.TotalAmount
        };

        return await _orderRepository.CreateOrderAsync(order);
    }
}
