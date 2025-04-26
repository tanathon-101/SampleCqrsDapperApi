using MediatR;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;

namespace SampleCqrsDapperApi.Application.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email
            };

            return await _customerRepository.CreateCustomerAsync(customer);
        }
    }
}