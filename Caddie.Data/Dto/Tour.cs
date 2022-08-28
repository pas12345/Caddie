using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Tour")]
    [PrimaryKey("TourId", AutoIncrement = true)]
    [ExplicitColumns]
    public class Tour
    {
        [Column("TourId")]
        public int Id { get; set; }

        [Column("TourDate")]
        public DateTime TourDate { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("LastRegistrationDate")]
        public DateTime LastRegistrationDate { get; set; }

        [Column("OpenForSignUp")]
        public bool OpenForSignUp { get; set; }

        [Column("MaxNoOfMembers")]
        public int MaxNoOfMembers { get; set; }

        [Column("NoOfMembers")]
        public int NoOfMembers { get; set; }

        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("UrlDescription")]
        public int UrlDescription { get; set; }

        [Column("SponsorLogoId")]
        public int SponsorLogoId { get; set; }

        [Column("Sponsor")]
        public string Sponsor { get; set; }
    }
}
