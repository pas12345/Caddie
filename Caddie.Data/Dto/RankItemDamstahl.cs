using PetaPoco;

namespace Caddie.Data.Dto
{
    [ExplicitColumns]
	public class RankItemDamstahl
	{
        [Column("Rank")]
        public int Rank { get; set; }
        [Column("VgcNo")]
        public int VgcNo { get; set; }
        [Column("Firstname")]
        public string Firstname { get; set; }
        [Column("Lastname")]
        public string Lastname { get; set; }
        [Column("DamstahlTotal")]
        public int Score { get; set; }
        [Column("DamstahlMin")]
        public int MinValue { get; set; }
        [Column("HcpGroup")]
        public int HcpGroup { get; set; }
    }
}
