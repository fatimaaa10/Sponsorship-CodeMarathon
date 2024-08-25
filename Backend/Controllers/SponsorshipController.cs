using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SponsorshipApp.DAO;
using SponsorshipApp.Models;

namespace SponsorshipApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorshipController : ControllerBase
    {
        private readonly ISponsorRepo _sponsorshipRepo;
        public SponsorshipController(ISponsorRepo sponsorshipRepo)   
        {
            _sponsorshipRepo = sponsorshipRepo;
        }

        [HttpGet("Sponsors List")]
        public async Task<ActionResult<List<SponsorsPayments>>> GetSponsors()
        {
            var sponsors = await _sponsorshipRepo.GetSponsors();
            return Ok(sponsors);
        }

        [HttpPost("Add Payment")]
        public async Task<ActionResult<int>> AddPayment(Payments payment)
        {
            var rowsInserted = await _sponsorshipRepo.AddPayment(payment);
            return Ok(rowsInserted);
        }

        [HttpGet ("Sponsor Payment Details")]
        public async Task<ActionResult<List<SponsorsPayments>>> GetSponsorDetails()
        {
            var sponsorDetails = await _sponsorshipRepo.GetSponsorDetails();
            return Ok(sponsorDetails);
        }

        [HttpGet("Match Details")]
        public async Task<ActionResult<List<Matches>>> GetMatchDetails()
        {
            var matchDetails = await _sponsorshipRepo.GetMatchDetails();
            return Ok(matchDetails);
        }

        [HttpGet("Sponsor Match Count")]
        public async Task<ActionResult<List<SponsorMatchCount>>> GetSponsorMatchCountsByYear(int year)
        {
            if(year <= 0)
            {
                return BadRequest("Invalid year parameter");
            }

            var result = await _sponsorshipRepo.GetSponsorMatchCountsByYear(year);
            return Ok(result);
        }
    }
}
