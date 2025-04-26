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
}
