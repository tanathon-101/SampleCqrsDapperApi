using MediatR;
using SampleCqrsDapperApi.Domain.Entities;

namespace SampleCqrsDapperApi.Application.Queries
{
    public class GetOrdersQuery : IRequest<IEnumerable<Order>>
    {
    }
}