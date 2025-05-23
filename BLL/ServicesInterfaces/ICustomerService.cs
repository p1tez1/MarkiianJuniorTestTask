using Model;
using Model.Customer;

namespace BLL.ServicesInterfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerModel>> GetOnlyTvCustomersAsync();
        Task<IEnumerable<CustomerModel>> GetOnlyDslCustomerAsync();
        Task<IEnumerable<OverlappingTvProductsModel>> GetOverlappingTvProducts();
        Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomer();
        Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomerOtherwise();
    }
}
