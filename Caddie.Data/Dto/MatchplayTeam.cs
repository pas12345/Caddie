using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Globalization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.LeagueTeam")]
    [PrimaryKey("LeagueTeamId", AutoIncrement = true)]
    [ExplicitColumns]

    public class MatchplayTeam
    {
        [Column("LeagueTeamId")]
        public int Id { get; set; }
        
        [Column("TeamName")]
        public string SerieId { get; set; }

        [Column("VgcNo")]
        public int VgcNo { get; set; }

        [Column("VgcNoPartner")]
        public int VgcNoPartner { get; set; }
    }
}
