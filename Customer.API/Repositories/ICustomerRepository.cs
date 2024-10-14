using Customer.API.Models.Domain;

namespace Customer.API.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDomain>> GetAllCustomersAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000);

        Task<CustomerDomain?> GetCustomerByIdAsync(Guid customerId);

        Task<CustomerDomain> CreateCustomerAsync(CustomerDomain customer);

        Task<CustomerDomain?> UpdateCustomerAsync(Guid customerId, CustomerDomain customer);

        Task<CustomerDomain?> DeleteCustomerAsync(Guid customerId);
    }
}
