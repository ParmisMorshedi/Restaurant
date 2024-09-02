using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs
{
    public class ReservationDTO
    {

        public int ReservationId { get; set; }
        [Required]
        public int TableId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public TimeOnly Time { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1, 150)]
        public int NumberOfGuests { get; set; }
           
    }
}
