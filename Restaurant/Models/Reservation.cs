using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Reservation
    {
      
        public int Id { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }
        public Table Table { get; set; } // Navigation property for the related Table entity.

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }// Navigation property for the related Customer entity.

     
        public TimeOnly Time {  get; set; }
     
        public DateTime Date { get; set; }
   
        public int NumberOfGuests { get; set; }
    }
}
