
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.IServices;

namespace Restaurant.Services
{
    public class CustomerService : ICustomerService
    {
        //Dependency injection
        private readonly ICustomerRepo _customerRepo;
        private readonly RestaurantContext _context;
        
        private readonly IReservationRepo _reservationRepo;

        // Constructor injection of dependencies
        public CustomerService(ICustomerRepo customerRepo, IReservationRepo reservationRepo,RestaurantContext context)
        {
            _customerRepo = customerRepo;
            _reservationRepo = reservationRepo;          
            _context = context;
        }

        // Add a new customer
        public async Task AddCustomersAsync(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                throw new ArgumentNullException(nameof(customerDTO), "CustomerDTO cannot be null.");
            }

            var customer = new Customer
            {
                Id = customerDTO.CustomerId,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber
            };


            try
            {
                await _customerRepo.AddCustomersAsync(customer);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while adding the customer: {ex.Message}");
                throw;
            }

        }

        // Delete a customer by ID
        public async Task<bool> DeleteCustomersAsync(int customerId)
        {

            try
            {
                return await _customerRepo.DeleteCustomersAsync(customerId);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while deleting the customer: {ex.Message}");
                throw;
            }
        }
        // Retrieve all customers
        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _customerRepo.GetAllCustomersAsync();
                return customers.Select(c => new CustomerDTO
                {
                    CustomerId = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving all customers: {ex.Message}");
                throw;
            }
        }

        // Retrieve a customer by ID
        public async Task<CustomerDTO> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                var customer = await _customerRepo.GetCustomerByIdsAsync(customerId);
                if (customer == null) return null;

                return new CustomerDTO
                {
                    CustomerId = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                };
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"An error occurred while retrieving the customer: {ex.Message}");
                throw;
            }
        }


        // Update an existing customer
        public async Task<bool> UpdateCustomersAsync(CustomerDTO customerDTO)
        {
            if (customerDTO == null)
            {
                throw new ArgumentNullException(nameof(customerDTO), "CustomerDTO cannot be null.");
            }

            var customer = new Customer
            {
                Id = customerDTO.CustomerId,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber
            };

            try
            {
                return await _customerRepo.UpdateCustomersAsync(customer);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while updating the customer: {ex.Message}");
                throw;
            };
        }
    }
}
