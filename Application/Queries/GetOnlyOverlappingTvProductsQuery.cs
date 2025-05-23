using MediatR;
using Model;

namespace Application.Queries
{
    public record GetOnlyOverlappingTvProductsQuery() : IRequest<IEnumerable<OverlappingTvProductsModel>>;
}
