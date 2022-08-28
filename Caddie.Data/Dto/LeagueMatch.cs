using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Tour")]
    [PrimaryKey("LeagueId", AutoIncrement = true)]
    [ExplicitColumns]

    public class LeagueMatch
    {
        [Column("LeagueId")]
		public int LeagueId { get; set; }

        [Column("LeagueName")]
        public string LeagueName { get; set; }

        [Column("Serie")]
        public int Serie { get; set; }

        [Column("Cup")]
        public int Cup { get; set; }

        [Column("Playround")]
        public int Playround { get; set; }

        [Column("Season")]
        public int Season { get; set; }

        [Column("LeagueMatchId")]
        public int LeagueMatchId { get; set; }

        [Column("MatchResult")]
        public int MatchResult { get; set; }

        [Column("ResultText")]
        public string ResultText { get; set; }

        [Column("TeamName1")]
        public string TeamName1 { get; set; }

        [Column("LeagueTeamId1")]
        public int LeagueTeamId1 { get; set; }

        [Column("TeamName2")]
        public string TeamName2 { get; set; }

        [Column("LeagueTeamId2")]
        public int LeagueTeamId2 { get; set; }
    }
}
