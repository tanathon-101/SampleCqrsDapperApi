using Microsoft.AspNetCore.Mvc;
using MediatR;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Domain.Entities;
using SampleCqrsDapperApi.Domain.Interfaces;

namespace SampleCqrsDapperApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(IMediator mediator, ICustomerRepository customerRepository)
        {
            _mediator = mediator;
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { CustomerId = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }
    }
}
