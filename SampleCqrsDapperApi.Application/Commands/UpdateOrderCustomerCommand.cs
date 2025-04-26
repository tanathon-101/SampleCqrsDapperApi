using MediatR;

namespace SampleCqrsDapperApi.Application.Commands
{
    public class UpdateOrderCustomerCommand : IRequest<bool>
    {
        public int OrderId { get; set; }
        public int NewCustomerId { get; set; }
    }
}