using MediatR;
using SampleCqrsDapperApi.Application.DTOs;

namespace SampleCqrsDapperApi.Application.Queries
{
    public class GetCustomerWithOrdersQuery : IRequest<IEnumerable<CustomerWithOrdersDto>>
    {
       
    }
}