using MediatR;
using Model;
using Model.Customer;

namespace Application.Queries
{
    public record GetOnlyMatchedCustomerQuery() : IRequest<IEnumerable<MatchedCustomerModel>>;
}
