using BLL.ServicesInterfaces;
using DAL.Interfaces;
using Model;
using Model.Customer;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerModel>> GetOnlyTvCustomersAsync()
        {
            return await _customerRepository.GetOnlyTvCustomerAsync();
        }

        public async Task<IEnumerable<CustomerModel>> GetOnlyDslCustomerAsync()
        {
            return await _customerRepository.GetOnlyDslCustomerAsync();
        }
        public async Task<IEnumerable<OverlappingTvProductsModel>> GetOverlappingTvProducts()
        {
            return await _customerRepository.GetOverlappingTvProducts();
        }
        public async Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomer()
        {
            var matchedForPost = await _customerRepository.FindMatchedCustomer();
            await _customerRepository.PostMatchedCustomersAsync(matchedForPost);
            return await _customerRepository.GetMatchedCustomer();
        }
        public async Task<IEnumerable<MatchedCustomerModel>> GetMatchedCustomerOtherwise()
        {
            return await _customerRepository.FindAndPostMatchedCustomersAsync();
        }
    }
}
