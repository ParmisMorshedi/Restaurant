using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Table
    {     
        public int Id { get; set; }

        public int Number { get; set; }
   
        public int Seats { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
