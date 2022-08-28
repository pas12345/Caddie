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

    public class League
    {
        [Column("TourDate")]
		public int LeagueId { get; set; }
        [Column("TourDate")]
        public string LeagueName { get; set; }
        [Column("TourDate")]
        public int Season { get; set; }
        [Column("TourDate")]
        public int SecondLeg { get; set; }
        [Column("TourDate")]
        public DateTime EndOfSeason { get; set; }
        [Column("TourDate")]
        public int Serie { get; set; }
        [Column("TourDate")]
        public int Playround { get; set; }
    }
}
