using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.vCompetitionResult")]
    [PrimaryKey("CompetitionResultId", AutoIncrement = true)]
    [ExplicitColumns]
	public class RankItemOther
	{
        [Column("CompetitionResultId")]
        public int Id { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }
        [Column("CompetitionText")]
        public string Name { get; set; }
    }
}
