namespace SponsorshipApp.Models
{
    public class Contracts
    {
        public int ContractId { get; set; }
        public int SponsorId { get; set; }
        public int MatchId { get; set; }
        public DateTime ContractDate { get; set; }
        public decimal ContractValue { get; set; }
    }
}
