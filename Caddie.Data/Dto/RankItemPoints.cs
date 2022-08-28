using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Tour")]
    [PrimaryKey("TourId", AutoIncrement = true)]
    [ExplicitColumns]

	public class RankItemPoints
	{
		[Column("TourDate")]
		public int VgcNo { get; set; }
		[Column("TourDate")]
		public string FullName { get; set; }
		[Column("TourDate")]
		public int Points { get; set; }
		[Column("TourDate")]
		public int Count { get; set; }
	}
}
