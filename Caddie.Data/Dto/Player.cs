using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Player")]
    [PrimaryKey("VgcNo", AutoIncrement = true)]
    [ExplicitColumns]
    public class Player
    {
        public Player()
        {
            HcpUpdated = new DateTime(2000, 1, 1);
        }
        [Column("PlayerId")]
        public int Id { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("MemberShipId")]
        public int MemberShipId { get; set; }
        [Column("IsMale")]
        public bool IsMale { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("ZipCode")]
        public string ZipCode { get; set; }
        [Column("City")]
        public string City { get; set; }
        [Column("Address")]
        public string Address { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Sponsor")]
        public bool Sponsor { get; set; }
        [Column("HcpIndex")]
        public decimal HcpIndex { get; set; }
        [Column("HcpUpdated")]
        public DateTime HcpUpdated { get; set; }
        [Column("Phone")]
        public string Phone { get; set; }
        [Column("NameGroup")]
        public int NameGroup { get; set; }
        [Column("Fee")]
        public bool Fee { get; set; }
        [Column("Insurance")]
        public bool Insurance { get; set; }
        [Column("Season")]
        public int Season { get; set; }
        [Column("Eclectic")]
        public bool Eclectic { get; set; }
        [Column("LastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}
