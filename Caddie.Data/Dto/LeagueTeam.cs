using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.vLeagueTeam")]
    [PrimaryKey("LeagueId", AutoIncrement = true)]
    [ExplicitColumns]

	public class LeagueTeam
	{
        [Column("LeagueId")]
        public int LeagueId { get; set; }

        [Column("LeagueName")]
        public string TeamLeagueName { get; set; }

        [Column("Single")]
        public bool IsSingle { get { return LeagueId == 1 || LeagueId == 2; } }

        [Column("LeagueTeamId")]
        public int LeagueTeamId { get; set; }

        [Column("Season")]
        public int Season { get; set; }


        [Column("TeamName")]
        public string TeamName { get; set; }

        [Column("VgcNo")]
        public int VgcNo { get; set; }

        [Column("VgcNoPartner")]
        public int? VgcNoPartner { get; set; }
    }
}
