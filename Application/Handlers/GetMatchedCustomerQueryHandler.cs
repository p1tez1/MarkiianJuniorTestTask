using Application.Queries;
using BLL.ServicesInterfaces;
using MediatR;
using Model;
using Model.Customer;

namespace Application.Handlers
{
    public class GetMatchedCustomerQueryHandler : IRequestHandler<GetOnlyMatchedCustomerQuery, IEnumerable<MatchedCustomerModel>>
    {
        private readonly ICustomerService _customerService;

        public GetMatchedCustomerQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<MatchedCustomerModel>> Handle(GetOnlyMatchedCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetMatchedCustomer();
        }
    }
}
