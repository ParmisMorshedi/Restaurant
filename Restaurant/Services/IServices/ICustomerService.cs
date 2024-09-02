using Restaurant.Models.DTOs;

namespace Restaurant.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task<CustomerDTO> GetCustomerByIdAsync(int customerId);
      
        Task AddCustomersAsync(CustomerDTO customerDTO);
        Task<bool> UpdateCustomersAsync(CustomerDTO customerDTO);
        Task<bool> DeleteCustomersAsync(int customerId);
        
    }
}
