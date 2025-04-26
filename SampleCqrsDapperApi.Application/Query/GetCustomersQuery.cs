using MediatR;
using SampleCqrsDapperApi.Application.DTOs;
using System.Collections.Generic;

namespace SampleCqrsDapperApi.Application.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
    }
}
