using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taste.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickUpTime { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Pick Up Date")]
        public DateTime PickUpDate { get; set; }

        public string? Status { get; set; }

        public string? PaymentStatus { get; set; }

        public string? Comments { get; set; }

        [Required]
        [Display(Name = "Pick Up Name")]
        public string PickName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string? TransactionId { get; set; }

    }
}
