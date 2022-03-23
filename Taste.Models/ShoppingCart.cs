using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }

        public int MenuItemId { get; set; }

        [NotMapped]
        [ForeignKey("MenuItemId")]
        public MenuItem? MenuItem { get; set; }

        [Range(0, 100, ErrorMessage = "count should be between 1 and 100")]
        public int Count { get; set; }
    }
}
