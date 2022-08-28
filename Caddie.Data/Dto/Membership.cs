using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [PrimaryKey("MembershipId", AutoIncrement = true)]
    [ExplicitColumns]

	public class Membership
	{
		[Column("VgcNo")]
		public int VgcNo { get; set; }

		[Column("Firstname")]
		public string Firstname { get; set; }

		[Column("Lastname")]
		public string Lastname { get; set; }

		[Column("City")]
		public string City { get; set; }

		[Column("ZipCode")]
		public string ZipCode { get; set; }

		[Column("Address")]
		public string Address { get; set; }

		[Column("Email")]
		public string Email { get; set; }

		[Column("Phone")]
		public string Phone { get; set; }

		[Column("CellPhone")]
		public string CellPhone { get; set; }

		[Column("Eclectic")]
		public bool Eclectic { get; set; }

		[Column("Fee")]
		public bool Fee { get; set; }

		[Column("Guest")]
		public bool Guest { get; set; }

		[Column("Id")]
		public int MembershipId { get; set; }

        [Column("Season")]
        public int Season { get; set; }

        [Column("NameGroup")]
        public int NameGroup { get; set; }

        [Column("HcpIndex")]
        public decimal HcpIndex { get; set; }

        [Column("HcpUpdated")]
        public DateTime HcpUpdated { get; set; }
    }
}
