using PetaPoco;
using System;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
    public class RankItemNearestPin
	{
        [Column("Rank")]
        public int Rank { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("PinName")]
        public string PinName { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("DistanceInCm")]
        public int DistanceInCm { get; set; }
        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }
	}
}
