using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs;

public class CustomerDTO
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]// Limits the length of the Name field to 100 characters.
    public string Name { get; set; }
    [EmailAddress]// Ensures that the Email field contains a valid email address.
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
  
}
