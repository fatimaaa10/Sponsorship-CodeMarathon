using System.ComponentModel.DataAnnotations;

namespace SponsorshipApp.Models
{
    public class Payments
    {
        public int PaymentId { get; set; }
        public int ContractId { get; set; }
        [Required]public DateTime PaymentDate { get; set; }
        [Required]public decimal AmountPaid { get; set; }
        public string PaymentStatus { get; set; }
    }
}
