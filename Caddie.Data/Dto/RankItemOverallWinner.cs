using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Globalization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
	public class RankItemOverallWinner
	{
        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("Points")]
        public int Score { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("ClubName")]
        public string ClubName { get; set; }
	}
}
