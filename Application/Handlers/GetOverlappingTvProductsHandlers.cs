using Application.Queries;
using BLL.ServicesInterfaces;
using MediatR;

namespace Application.Handlers
{

    public class GetOverlappingTvProductsHandlers : IRequestHandler<GetOnlyOverlappingTvProductsQuery, IEnumerable<Model.OverlappingTvProductsModel>>
    {
        private readonly ICustomerService _customerService;

        public GetOverlappingTvProductsHandlers(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<Model.OverlappingTvProductsModel>> Handle(GetOnlyOverlappingTvProductsQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetOverlappingTvProducts();
        }
    }

}
