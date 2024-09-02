using Microsoft.EntityFrameworkCore;
using Restaurant.Data.Repositories.IRepositories;
using Restaurant.Models;

namespace Restaurant.Data.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly RestaurantContext _context;

        public CustomerRepo(RestaurantContext context) 
        {
            _context = context;
        }

        // Adds a new customer to the database asynchronously.
        public async Task AddCustomersAsync(Customer customer)
        {
            // Ensure the customer object is not null.
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }
            try
            {
                // Add the customer to the context and save changes asynchronously.
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception message.
                Console.WriteLine($"An error occurred while adding the customer: {ex.Message}");
                throw;
            }
        }

        // Deletes a customer by ID asynchronously. Returns true if the deletion was successful.
        public async Task<bool> DeleteCustomersAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                // Return false if the customer was not found.
                return false; 
            }

            _context.Customers.Remove(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        // Retrieves all customers asynchronously.
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        // Retrieves a customer by ID asynchronously.
        public async Task<Customer> GetCustomerByIdsAsync(int customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        // Retrieves all reservations for a specific customer by customer ID asynchronously.
        //public async Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId)
        //{
        //    return await _context.Reservations
        //    .Where(r => r.CustomerId == customerId)
        //    .ToListAsync();

        //}


        // Updates an existing customer asynchronously. Returns true if the update was successful.
        public async Task<bool> UpdateCustomersAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }

            try
            {
                // Mark the customer as modified and save changes asynchronously.
                _context.Customers.Update(customer);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred while updating the customer: {ex.Message}");
                throw;
            }   
        }
    }
}
