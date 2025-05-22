using Application.Queries;
using BLL.ServicesInterfaces;
using MediatR;
using Model.Customer;

namespace Application.Handlers
{
    public class GetOnlyDslCustomersHandler : IRequestHandler<GetOnlyDslCustomersQuery, IEnumerable<CustomerModel>>
    {
        private readonly ICustomerService _customerService;

        public GetOnlyDslCustomersHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerModel>> Handle(GetOnlyDslCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerService.GetOnlyDslCustomerAsync();
        }
    }

}
