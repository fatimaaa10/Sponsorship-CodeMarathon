namespace SponsorshipApp.Models
{
    public class Matches
    {
        public int MatchId { get; set; }
        public string MatchName { get; set; }
        public DateTime MatchDate { get; set; }
        public string Location { get; set; }
        public decimal TotalPayments {  get; set; }
        
    }
}
