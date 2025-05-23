using Application.Queries;
using BLL.ServicesInterfaces;
using MediatR;
using Model;
using Model.Customer;

namespace Application.Handlers
{
    public class GetMatchedCustomerQueryHandlerOtherwise : IRequestHandler<GetOnlyMatchedCustomerQueryOtherwise, IEnumerable<MatchedCustomerModel>>
    {
        private readonly ICustomerService _customerService;

        public GetMatchedCustomerQueryHandlerOtherwise(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<MatchedCustomerModel>> Handle(GetOnlyMatchedCustomerQueryOtherwise request, CancellationToken cancellationToken)
        {
            return await _customerService.GetMatchedCustomerOtherwise();
        }
    }
}
