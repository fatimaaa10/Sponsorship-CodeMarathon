namespace SponsorshipApp.Models
{
    public class SponsorsPayments
    {
        public int SponsorId { get; set; }
        public decimal TotalPaymentsMade { get; set; }
        public int NumberOfPayments {  get; set; }
        public DateTime LatestPaymentDate { get; set; }
    }
}
