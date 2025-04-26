using MediatR;

namespace SampleCqrsDapperApi.Application.Commands
{
    public class CreateCustomerCommand : IRequest<int> 
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}