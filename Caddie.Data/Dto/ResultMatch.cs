using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    //[TableName("ms.Tour")]
    [PrimaryKey("MsResultId", AutoIncrement = true)]
    [ExplicitColumns]
	public class ResultMatch
	{
        public ResultMatch()
        {
            Id = 0;
        }
        [Column("MatchResultId")]
        public int Id { get; set; }

        [Column("MatchId")]
        public int MatchId { get; set; }
        [Column("MemberShipId")]
        public int MemberShipId { get; set; }
        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }
        [Column("Tee")]
        public string Tee { get; set; }
        [Column("ClubName")]
        public string ClubName { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("Matchform")]
        public string Matchform { get; set; }
        [Column("MatchformId")]
        public int MatchformId { get; set; }
        [Column("Official")]
        public bool Official { get; set; }
        [Column("Par")]
        public int Par { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("HcpGroup")]
        public int HcpGroup { get; set; }
        [Column("Rank")]
        public int Rank { get; set; }
        [Column("No")]
        public int No { get; set; }
        [Column("OverallWinner")]
        public int OverallWinner { get; set; }
        [Column("Dining")]
        public bool Dining { get; set; }
        [Column("Hcp")]
        public int Hcp { get; set; }

        [Column("MaxA")]
        public int MaxA { get; set; }

        [Column("HcpIndex")]
        public decimal HcpIndex { get; set; }
        [Column("Brutto")]
        public int? Brutto { get; set; }
        [Column("Netto")]
        public int? Netto { get; set; }
        [Column("Hallington")]
        public int? Hallington { get; set; }
        [Column("Points")]
        public int Points { get; set; }
        [Column("DamstahlPoints")]
        public int DamstahlPoints { get; set; }
        [Column("Puts")]
        public int? Puts { get; set; }
        [Column("Birdies")]
        public int Birdies { get; set; }


        [Column("ShootOut")]
        public int? ShootOut { get; set; }

        [Column("timestamp")]
        public byte[] Rowversion { get; set; }
    }
    public class ResultMember
    {
        public ResultMember()
        {
            ;
        }
        public int Hcp { get { return 99; } set {; } }
        public int? Birdies { get { return 99; } set {; } }
        public string Clubname { get; set; }
        public string Coursename { get; set; }
    }

}
