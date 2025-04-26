using MediatR;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Domain.Interfaces;

namespace SampleCqrsDapperApi.Application.Handlers
{
    public class UpdateOrderCustomerCommandHandler : IRequestHandler<UpdateOrderCustomerCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCustomerCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(UpdateOrderCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateOrderCustomerAsync(request.OrderId, request.NewCustomerId);
        }
    }
}