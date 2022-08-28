using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.Club")]
    [PrimaryKey("ClubId", AutoIncrement = true)]
    [ExplicitColumns]

    public class Club
    {
        [Column("ClubId")]
        public int Id { get; set; }
        [Column("ClubName")]
        public string Name { get; set; }
    }
}
