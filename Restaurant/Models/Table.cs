using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 15)]// Validates that the value of Number is between 1 and 15, inclusive.
        public int Number { get; set; }
        [Required]
        [Range(1, 10)]
        public int Seats { get; set; }

        // Navigation property for the related Reservations. A Table can have multiple Reservations.
        public ICollection<Reservation> Reservations { get; set; }
    }
}
