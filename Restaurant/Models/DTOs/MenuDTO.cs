using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }

        [Required]
        public string DishName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
    }
}
