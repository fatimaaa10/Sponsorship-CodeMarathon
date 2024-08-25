using SponsorshipApp.Models;

namespace SponsorshipApp.DAO
{
    public interface ISponsorRepo
    {
        Task<List<Sponsors>>  GetSponsors();
        Task<int> AddPayment(Payments payment);
        Task<List<SponsorsPayments>> GetSponsorDetails();
        Task<List<Matches>> GetMatchDetails();
        Task<List<SponsorMatchCount>> GetSponsorMatchCountsByYear(int year);

    }
}
