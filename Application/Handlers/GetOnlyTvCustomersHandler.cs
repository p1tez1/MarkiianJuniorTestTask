using Application.Queries;
using BLL.ServicesInterfaces;
using MediatR;
using Model;
using Model.Customer;

namespace Application.Handlers
{
    public class GetOnlyTvCustomersHandler : IRequestHandler<GetOnlyTvCustomersQuery, IEnumerable<CustomerModel>>
    {
        private readonly ICustomerService _customerService;

        public GetOnlyTvCustomersHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerModel>> Handle(GetOnlyTvCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetOnlyTvCustomersAsync();
        }
    }
}
