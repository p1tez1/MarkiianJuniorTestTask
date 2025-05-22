using MediatR;
using Model;
using Model.Customer;

namespace Application.Queries
{
    public record GetOnlyDslCustomersQuery() : IRequest<IEnumerable<CustomerModel>>;
}
