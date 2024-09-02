using Restaurant.Models;

namespace Restaurant.Data.Repositories.IRepositories
{
    public interface ICustomerRepo
    {
        // Retrieves all customers asynchronously.
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        // Retrieves a customer by their ID asynchronously.
        Task<Customer> GetCustomerByIdsAsync(int customerId);
        Task AddCustomersAsync(Customer customer);

        // Deletes a customer by their ID asynchronously. Returns true if successful.
        Task<bool> DeleteCustomersAsync(int cuestomerId);
        Task<bool> UpdateCustomersAsync(Customer customer);

        //Task<IEnumerable<Reservation>> GetReservationsByCustomerIdAsync(int customerId);
    }
}
