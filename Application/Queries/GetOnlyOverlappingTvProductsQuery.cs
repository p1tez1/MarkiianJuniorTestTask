using MediatR;
using Model;
using Model.Customer;

namespace Application.Queries
{
    public record GetOnlyOverlappingTvProductsQuery() : IRequest<IEnumerable<OverlappingTvProductsModel>>;
}
