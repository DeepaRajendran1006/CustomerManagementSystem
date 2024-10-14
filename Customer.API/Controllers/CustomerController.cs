using AutoMapper;
using Customer.API.CustomActionFilters;
using Customer.API.Models.Domain;
using Customer.API.Models.DTO;
using Customer.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// CREATE CUSTOMER
        /// </summary>
        /// <param name="addCustomerDto">Customer data to add</param>
        /// <returns>Added Customer Data</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateCustomer([FromBody] AddOrUpdateCustomerDto addCustomerDto)
        {
            // Convert DTO to Domain Model
            var customer_domain = mapper.Map<CustomerDomain>(addCustomerDto);

            // Create Customer
            customer_domain = await customerRepository.CreateCustomerAsync(customer_domain);

            // Return the coverted DTO 
            return Ok(mapper.Map<CustomerDto>(customer_domain));
        }

        /// <summary>
        /// GET ALL CUSTOMERS
        /// </summary>
        /// <param name="filterOn">Column name to filter</param>
        /// <param name="filterQuery">Query to filter</param>
        /// <param name="sortBy">Column name to sort</param>
        /// <param name="isAscending">True if Ascending or False if Desending</param>
        /// <param name="pageNumber">Page number to display</param>
        /// <param name="pageSize">Number of records to display in the page number</param>
        /// <returns>List of Customers</returns>

        [HttpGet]
        // GET : /api/customers?filterOn=Name&filterQuery=Emug&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        public async Task<IActionResult> GetAllCustomers([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy,
                                [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            // Get all the customers
            var customers_domain = await customerRepository.GetAllCustomersAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Return the converted Customers DTO
            return Ok(mapper.Map<List<CustomerDto>>(customers_domain));
        }

        /// <summary>
        /// GET CUSTOMER BY ID
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Customer Information</returns>
        [HttpGet]
        [Route("{customerId:guid}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid customerId)
        {
            // Get the Customer data by Id
            var customer_domain = await customerRepository.GetCustomerByIdAsync(customerId);

            if (customer_domain == null)
            {
                return NotFound();
            }

            // Return the converted Customer DTO
            return Ok(mapper.Map<CustomerDto>(customer_domain));
        }

        /// <summary>
        /// UPDATE CUSTOMER
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="updateCustomerDto">Customer Data</param>
        /// <returns>Updated Customer Data</returns>
        [HttpPut]
        [Route("{customerId:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid customerId, [FromBody] AddOrUpdateCustomerDto updateCustomerDto)
        {
            // Convert DTO to Domain model to send
            var customer_domain = mapper.Map<CustomerDomain>(updateCustomerDto);

            // Update the customer information
            customer_domain = await customerRepository.UpdateCustomerAsync(customerId, customer_domain);

            if (customer_domain == null)
            {
                return NotFound();
            }

            // Return the converted Customer DTO
            return Ok(mapper.Map<CustomerDto>(customer_domain));

        }

        /// <summary>
        /// DELETE CUSTOMER
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <returns>Deleted Customer Data</returns>
        [HttpDelete]
        [Route("{customerId:guid}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        {
            // Delete the customer using customer Id
            var customer_domain = await customerRepository.DeleteCustomerAsync(customerId);

            if (customer_domain == null)
            {
                return NotFound();
            }

            // Return the deleted Customer DTO
            return Ok(mapper.Map<CustomerDto>(customer_domain));
        }
    }
}
