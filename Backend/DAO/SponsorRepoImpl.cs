using System.Data;
using System.Text.RegularExpressions;
using Npgsql;
using NpgsqlTypes;
using SponsorshipApp.Models;

namespace SponsorshipApp.DAO
{
    public class SponsorRepoImpl : ISponsorRepo
    {

        private readonly NpgsqlConnection _connection;

        public SponsorRepoImpl(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<Sponsors>> GetSponsors()
        {
            string query = @"select * from sponsorship.sponsors";
            List<Sponsors> slist = new List<Sponsors>();
            string errorMessage = string.Empty;
            Sponsors? s = null;

            try
            {
                using (_connection) 
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            s = new Sponsors();
                            s.SponsorId = reader.GetInt32(0);
                            s.SponsorName = reader.GetString(1);
                            s.IndustryType = reader.GetString(2);
                            s.ContactEmail = reader.GetString(3);
                            s.Phone = reader.GetString(4);
                            slist.Add(s);
                        }
                    }
                    reader.Close();
                }
                
            }
            catch (NpgsqlException e)
            {
                errorMessage = e.Message;
                Console.WriteLine("Exception: " + errorMessage);
            }

            return slist;
        }

        public async Task<int> AddPayment(Payments payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            if (string.IsNullOrEmpty(payment.PaymentDate.ToString()) || payment.AmountPaid == 0)
            {
                throw new ArgumentException("PaymentDate and AmountPaid are required");
            }

            int rowsInserted = 0;
            string message;

            string insertQuery = @$"insert into sponsorship.payments (contractid, paymentdate, amountpaid, paymentstatus) values ({payment.ContractId},'{payment.PaymentDate}',{payment.AmountPaid},'{payment.PaymentStatus}')";
            Console.WriteLine("Query: " + insertQuery);
            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, _connection);
                    insertCommand.CommandType = CommandType.Text;
                    rowsInserted = await insertCommand.ExecuteNonQueryAsync();
                }
            }

            catch (NpgsqlException ex)
            {
                message = ex.Message;
                Console.WriteLine("Exception: " + message);
            }

            return rowsInserted;

        }

        public async Task<List<SponsorsPayments>> GetSponsorDetails()
        {
            string query = @"SELECT s.SponsorId, 
                            SUM(p.AmountPaid) AS TotalPaymentsMade, 
                            COUNT(p.PaymentId) AS NumberOfPayments,
                            MAX(p.PaymentDate) AS LatestPaymentDate
                            FROM sponsorship.Sponsors s JOIN sponsorship.contracts c ON s.sponsorid = c.sponsorid
                            JOIN sponsorship.Payments p ON c.ContractId = p.ContractId
                            GROUP BY s.SponsorId";
            
            List<SponsorsPayments> sponsorDetails = new List<SponsorsPayments>();
            SponsorsPayments? sponsorDetail = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            sponsorDetail = new SponsorsPayments();
                            sponsorDetail.SponsorId = reader.GetInt32(0);
                            sponsorDetail.TotalPaymentsMade = reader.GetDecimal(1);
                            sponsorDetail.NumberOfPayments = reader.GetInt32(2);
                            sponsorDetail.LatestPaymentDate = reader.GetDateTime(3);

                            sponsorDetails.Add(sponsorDetail);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Console.WriteLine("Exception: " + errorMessage);
            }
            
            return sponsorDetails;
        }

        public async Task<List<Matches>> GetMatchDetails()
        {
            string query = @"select m.matchid, m.matchname, m.matchdate, m.location, 
                            SUM(p.amountpaid) as TotaAmountPaid
                            FROM sponsorship.matches m JOIN sponsorship.contracts c ON m.matchid = c.matchid
                            JOIN sponsorship.payments p ON c.contractid = p.contractid
                            GROUP BY m.matchid, m.matchname, m.matchdate, m.location
                            ORDER BY m.matchid ASC";

            List<Matches> matchList = new List<Matches>();
            Matches? matches = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            matches = new Matches();
                            matches.MatchId = reader.GetInt32(0);
                            matches.MatchName = reader.GetString(1);
                            matches.MatchDate = reader.GetDateTime(2);
                            matches.Location = reader.GetString(3);
                            matches.TotalPayments = reader.GetDecimal(4);

                            matchList.Add(matches);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Console.WriteLine("Exception: " + errorMessage);
            }
            return matchList;
        }

        public async Task<List<SponsorMatchCount>> GetSponsorMatchCountsByYear(int year)
        {
            string query = @"SELECT s.SponsorName, COUNT(c.MatchId) AS MatchCount
                            FROM sponsorship.Sponsors s
                            JOIN sponsorship.Contracts c ON s.SponsorId = c.SponsorId
                            JOIN sponsorship.Matches m ON c.MatchId = m.MatchId
                            WHERE EXTRACT(YEAR FROM m.MatchDate) = @Year
                            GROUP BY s.SponsorName";

            List<SponsorMatchCount> sponsorMatchCountsList = new List<SponsorMatchCount>();
            SponsorMatchCount sMC = null;

            try
            {
                using (_connection)
                {
                    await _connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand(query, _connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Year", year);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sMC = new SponsorMatchCount();
                            sMC.SponsorName = reader.GetString(0);
                            sMC.MatchCount = reader.GetInt32(1);

                            sponsorMatchCountsList.Add(sMC);
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                Console.WriteLine("Exception: " + errorMessage);
            }

            return sponsorMatchCountsList;
        }
    }
        
}
