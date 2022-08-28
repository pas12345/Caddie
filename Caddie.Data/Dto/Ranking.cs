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

	public class Ranking
	{
		[Column("TourDate")]
		public int MatchId { get; set; }
		[Column("TourDate")]
		public string League { get; set; }
		[Column("TourDate")]
		public string Ranks { get; set; }
		[Column("TourDate")]
		public int Winnner { get; set; }
	}
}
