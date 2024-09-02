using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DishName { get; set; }
        [Required]
        public decimal Price {  get; set; } 
        [Required]
        public bool IsAvailable { get; set; }

     
    }
}
