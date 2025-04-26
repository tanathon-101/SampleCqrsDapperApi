using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Application.Queries;

namespace SampleCqrsDapperApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { OrderCreated = result });
    }
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _mediator.Send(new GetOrdersQuery());
        return Ok(orders);
    }
    [HttpPut("ChangeCustomer")]
    public async Task<IActionResult> ChangeOrderCustomer([FromBody] UpdateOrderCustomerCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
            return Ok(new { Message = "Order's Customer updated successfully" });
        else
            return BadRequest(new { Message = "Failed to update Order's Customer" });
    }
}
