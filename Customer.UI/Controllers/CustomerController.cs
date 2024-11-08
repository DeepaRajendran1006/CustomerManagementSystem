﻿using Customer.UI.Models;
using Customer.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

namespace Customer.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get all the customers and list
        /// </summary>
        /// <returns>All Customers</returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            ViewBag.ErrorMessage = string.Empty;
            List<CustomerDto> customers = new List<CustomerDto>();
            try
            {
                var clientFactory = httpClientFactory.CreateClient();

                // Get customer list from API
                var responseMessage = await clientFactory.GetAsync("https://localhost:7269/api/Customer");
                responseMessage.EnsureSuccessStatusCode();

                var customer_list = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>() ?? Enumerable.Empty<CustomerDto>();
                customers.AddRange(customer_list);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View();
            }

            return View(customers);
        }

        /// <summary>
        /// Displays the Add new customer view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Save the newly created customer data
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddOrUpdateCustomerViewModel customerViewModel)
        {
            ViewBag.ErrorMessage = string.Empty;
            List<CustomerDto> customers = new List<CustomerDto>();
            try
            {
                var clientFactory = httpClientFactory.CreateClient();

                // Get customer list from API
                var responseMessage = await clientFactory.GetAsync("https://localhost:7269/api/Customer");
                responseMessage.EnsureSuccessStatusCode();

                var customer_list = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>() ?? Enumerable.Empty<CustomerDto>();
                customers.AddRange(customer_list);

                // Check if the existing customer list contains a customer with the same email and phone number
                bool customerExists = customers.Any(c =>
                                                (c.PhoneNumber == customerViewModel.PhoneNumber) ||
                                                string.Equals(c.Email, customerViewModel.Email, StringComparison.OrdinalIgnoreCase));

                if (!customerExists)
                {
                    // Form a request message to add a new customer
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://localhost:7269/api/Customer"),
                        Content = new StringContent(JsonSerializer.Serialize(customerViewModel), Encoding.UTF8, "application/json")
                    };

                    // Call the API to add a customer
                    responseMessage = await clientFactory.SendAsync(httpRequestMessage);
                    responseMessage.EnsureSuccessStatusCode();

                    // If successfull, redirect to List page
                    var response = await responseMessage.Content.ReadFromJsonAsync<CustomerDto>();

                    if (response != null)
                    {
                        return RedirectToAction("List", "Customer");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Customer already exists with this email or phone number.";
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View();
            }

            return View();            
        }

        /// <summary>
        /// Display the selected customer information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid customerId)
        {
            ViewBag.Title = string.Empty;
            var client = httpClientFactory.CreateClient();

            // Get the selected customer data
            var response = await client.GetFromJsonAsync<CustomerDto>($"https://localhost:7269/api/Customer/{customerId.ToString()}");

            if (response != null)
            {
                var viewModel = new AddOrUpdateCustomerViewModel
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    Email = response.Email,
                    City = response.City,
                    PhoneNumber = response.PhoneNumber,
                    Postcode = response.Postcode,
                    Address = response.Address,
                    Country = response.Country,
                    IsActive = response.IsActive
                };
                ViewBag.Title = string.Format("Customer Information - {0} {1}", response.FirstName, response.LastName);

                return View(viewModel);
            }

            return View(null);
        }

        /// <summary>
        /// Update the selected customer data
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(AddOrUpdateCustomerViewModel customerViewModel)
        {
            List<CustomerDto> customers = new List<CustomerDto>();
            ViewBag.ErrorMessage = string.Empty;
            try
            {
                var client = httpClientFactory.CreateClient();

                // Get customer list from API
                var responseMessage = await client.GetAsync("https://localhost:7269/api/Customer");
                responseMessage.EnsureSuccessStatusCode();

                var customer_list = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>() ?? Enumerable.Empty<CustomerDto>();
                customers.AddRange(customer_list);

                // Check if the existing customer list contains a customer with the same email and phone number
                bool customerExists = customers.Any(c =>
                                                (c.Id != customerViewModel.Id) &&
                                                ((c.PhoneNumber == customerViewModel.PhoneNumber) ||
                                                string.Equals(c.Email, customerViewModel.Email, StringComparison.OrdinalIgnoreCase)));

                if (!customerExists)
                {
                    // Form the request message to update the customer data
                    var httpRequestMessage = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri($"https://localhost:7269/api/Customer/{customerViewModel.Id}"),
                        Content = new StringContent(JsonSerializer.Serialize(customerViewModel), Encoding.UTF8, "application/json")
                    };

                    // Call the API to update a customer
                    var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    var response = await httpResponseMessage.Content.ReadFromJsonAsync<CustomerDto>();

                    // if updated successfully, redirect to list page
                    if (response != null)
                    {
                        return RedirectToAction("List", "Customer");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Customer already exists with this email or phone number.";
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View();
            }

            return View(customerViewModel);
        }

        /// <summary>
        /// Delete the customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(CustomerDto customer)
        {
            ViewBag.ErrorMessage = string.Empty;
            try
            {
                var client = httpClientFactory.CreateClient();

                // Call the API to delete a customer
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7269/api/Customer/{customer.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();

                // Return to the List page
                return RedirectToAction("List", "Customer");
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request.";
            }
            return View(customer);
        }

        /// <summary>
        /// In case of Error
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
