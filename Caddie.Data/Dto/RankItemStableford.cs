﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
	public class RankItemStableford
	{
        [Column("Rank")]
        public int Rank { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("Total")]
        public int Total { get; set; }
        [Column("Average")]
        public int Average { get; set; }
        [Column("Cnt")]
        public int Count { get; set; }
	}
}
