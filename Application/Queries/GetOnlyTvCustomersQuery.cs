using MediatR;
using Model.Customer;

namespace Application.Queries
{
    public record GetOnlyTvCustomersQuery() : IRequest<IEnumerable<CustomerModel>>;
}
