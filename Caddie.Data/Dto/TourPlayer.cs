using System;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
    public class TourPlayer
    {
        [Column("TourId")]
        public int TourId { get; set; } 

        [Column("VgcNo")]
        public int VgcNo { get; set; }

        [Column("Firstname")]
        public string Firstname { get; set; }

        [Column("Lastname")]
        public string Lastname { get; set; }

        [Column("HcpIndex")]
        public decimal HcpIndex { get; set; }

        [Column("SignedUp")]
        public bool SignedUp { get; set; }

        [Column("LastUpdateBy")]
        public string LastUpdateBy { get; set; }

        [Column("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
