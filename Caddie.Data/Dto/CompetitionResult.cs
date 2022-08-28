using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Competition")]
    [PrimaryKey("CompetitionId", AutoIncrement = true)]
    [ExplicitColumns]

    public class CompetitionResult
    {
        [Column("CompetitionResultId")]
        public int Id { get; set; }
        [Column("MatchId")]
        public int MatchId { get; set; }
        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }
        [Column("CompetitionId")]
        public int CompetitionId { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("MembershipId")]
        public int MembershipId { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("CompetitionText")]
        public string CompetitionText { get; set; }
    }
}
