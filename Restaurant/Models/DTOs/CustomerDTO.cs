using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs;

public class CustomerDTO
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
  
}
