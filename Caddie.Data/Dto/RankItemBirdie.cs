using PetaPoco;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
	public class RankItemBirdie
	{
        [Column("Rank")]
        public int Rank { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("Birdies")]
        public decimal Score { get; set; }
        [Column("Cnt")]
        public int Count { get; set; }
	}
}
