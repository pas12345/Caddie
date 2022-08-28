using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    //[TableName("ms.vMatch")]
    [PrimaryKey("MatchId")]
    [ExplicitColumns]
    public class Match
    {
        [Column("MatchId")]
        public int Id { get; set; }

        [Column("MatchDate")]
        public DateTime MatchDate { get; set; }

        [Column("MatchformId")]
        public int MatchformId { get; set; }

        [Column("Matchform")]
        public string Matchform { get; set; }

        [Column("MatchText")]
        public string MatchText { get; set; }

        [Column("IsHallington")]
        public bool IsHallington { get; set; }

        [Column("IsStrokePlay")]
        public bool IsStrokePlay { get; set; }

        [Column("Sponsor")]
        public string Sponsor { get; set; }

        [Column("SponsorLogoId")]
        public int SponsorLogoId { get; set; }

        [Column("CourseDetailId")]
        public int CourseDetailId { get; set; }

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("Par")]
        public int Par { get; set; }

        [Column("Tee")]
        public string Tee { get; set; }

        [Column("CourseRating")]
        public decimal CourseRating { get; set; }

        [Column("Slope")]
        public int Slope { get; set; }

        [Column("Remarks")]
        public string Remarks { get; set; }

        [Column("Official")]
        public bool Official { get; set; }

        [Column("Shootout")]
        public bool Shootout { get; set; }

        [Column("ClubName")]
        public string ClubName { get; set; }

        [Column("MatchRowversion")]
        public byte[] MatchRowversion { get; set; }
    }
}
