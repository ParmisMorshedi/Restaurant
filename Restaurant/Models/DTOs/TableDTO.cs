using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs
{
    public class TableDTO
    {
        public int TableId { get; set; }

        [Required]
        [Range(1, 15)]// Validates that the value of Number is between 1 and 15, inclusive.
        public int Number { get; set; }

        [Required]
        [Range(1, 10)]
        public int Seats { get; set; }
    }
}
