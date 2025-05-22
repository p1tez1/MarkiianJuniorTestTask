using Model;
using Model.Customer;

namespace DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetOnlyTvCustomerAsync();
        Task<IEnumerable<CustomerModel>> GetOnlyDslCustomerAsync();
        Task<IEnumerable<OverlappingTvProductsModel>> GetOverlappingTvProducts();
        Task<IEnumerable<MatchedCustomerModel>> FindMatchedCustomer();
        Task PostMatchedCustomersAsync(IEnumerable<MatchedCustomerModel> models);
        Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomer();
    }
}
