
namespace Caddie.Models
{
    public class MatchplayTeamModel
    {
        public int LeagueTeamId { get; set; }
        public string TeamName { get; set; }
        public int VgcNo { get; set; }
        public int Season { get; set; }
        public int LeagueId { get; set; }
        public int? VgcNoPartner { get; set; }
    }

}
