using MediatR;
using Model.Customer;

namespace Application.Queries
{
    public record GetOnlyMatchedCustomerQueryOtherwise() : IRequest<IEnumerable<MatchedCustomerModel>>;
}
