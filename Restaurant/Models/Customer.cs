using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)] // Limits the length of the Name field to 100 characters.
        public string Name { get; set; }

        [EmailAddress]// Ensures that the Email field contains a valid email address.
        public string Email{ get; set; }

        [Phone]// Ensures that the PhoneNumber field contains a valid phone number.
        public string PhoneNumber { get; set; }

        // Navigation property for the related Reservations. A Customer can have multiple Reservations
        public ICollection<Reservation> Reservations { get; set; }
    }
}
