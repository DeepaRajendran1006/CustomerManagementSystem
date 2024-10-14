using Customer.API.Data;
using Customer.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public SqlCustomerRepository(CustomerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="customer">Customer Data</param>
        /// <returns>Added Customer</returns>
        public async Task<CustomerDomain> CreateCustomerAsync(CustomerDomain customer)
        {
            // Update the created date and modified date for a customer
            customer.CreatedDate = DateTime.UtcNow;
            customer.ModifiedDate = DateTime.UtcNow;

            // Create customer in dbContext
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            return customer;
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Deleted Customer</returns>
        public async Task<CustomerDomain?> DeleteCustomerAsync(Guid customerId)
        {
            // Delete the customer
            var existing_customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            if (existing_customer != null)
            {
                dbContext.Customers.Remove(existing_customer);
                await dbContext.SaveChangesAsync();
            }

            return existing_customer;
        }

        /// <summary>
        /// Get all Customers
        /// </summary>
        /// <param name="filterOn">Column name to filter</param>
        /// <param name="filterQuery">Query to filter</param>
        /// <param name="sortBy">Column name to sort</param>
        /// <param name="isAscending">True if Ascending or False if Desending</param>
        /// <param name="pageNumber">Page number to display</param>
        /// <param name="pageSize">Number of records to display in the page number</param>
        /// <returns>List of Customers</returns>
        public async Task<List<CustomerDomain>> GetAllCustomersAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            // Get all customers
            var customers = dbContext.Customers.AsQueryable();

            //Filtering based on Name
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    customers = customers.Where(x => x.FirstName.Contains(filterQuery));
                }
                else if (filterOn.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    customers = customers.Where(x => x.LastName.Contains(filterQuery));
                }
            }
            // Sorting based on SortBy column
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                {
                    customers = isAscending ? customers.OrderBy(x => x.FirstName) : customers.OrderByDescending(x => x.FirstName);
                }

                if (sortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                {
                    customers = isAscending ? customers.OrderBy(x => x.LastName) : customers.OrderByDescending(x => x.LastName);
                }

                if (sortBy.Equals("City", StringComparison.OrdinalIgnoreCase))
                {
                    customers = isAscending ? customers.OrderBy(x => x.City) : customers.OrderByDescending(x => x.City);
                }

                if (sortBy.Equals("Country", StringComparison.OrdinalIgnoreCase))
                {
                    customers = isAscending ? customers.OrderBy(x => x.Country) : customers.OrderByDescending(x => x.Country);
                }

                if (sortBy.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    customers = isAscending ? customers.OrderBy(x => x.IsActive) : customers.OrderByDescending(x => x.IsActive);
                }
            }

            // calculate the number of records to be skipped for each page
            var skipRecords = (pageNumber - 1) * pageSize;

            // display only the number of records requested for the current page
            return await customers.Skip(skipRecords).Take(pageSize).ToListAsync();
        }

        /// <summary>
        /// Get Customer By Id
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Customer Data</returns>
        public async Task<CustomerDomain?> GetCustomerByIdAsync(Guid customerId)
        {
            // Get the customer by id
            CustomerDomain? customer_domain = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            return customer_domain;
        }

        /// <summary>
        /// Update Customer By Id
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="customer">Customer Data</param>
        /// <returns>Updated Customer</returns>
        public async Task<CustomerDomain?> UpdateCustomerAsync(Guid customerId, CustomerDomain customer)
        {
            //update the customer
            var existing_customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customerId);

            if (existing_customer != null)
            {
                existing_customer.FirstName = customer.FirstName;
                existing_customer.LastName = customer.LastName;
                existing_customer.Email = customer.Email;
                existing_customer.PhoneNumber = customer.PhoneNumber;
                existing_customer.City = customer.City;
                existing_customer.Address = customer.Address;
                existing_customer.Postcode = customer.Postcode;
                existing_customer.Country = customer.Country;
                existing_customer.IsActive = customer.IsActive;
                existing_customer.ModifiedDate = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();
            }
            return existing_customer;
        }
    }
}
