using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string? Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price should be more than 1$.")]
        public double Price { get; set; }

        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }

        [ForeignKey("FoodTypeId")]
        public FoodType? FoodType { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
