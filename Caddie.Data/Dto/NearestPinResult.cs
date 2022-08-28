using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.NearestPin")]
    [PrimaryKey("NearestPinId", AutoIncrement = true)]
    [ExplicitColumns]
    public class NearestPinResult
    {
        [Column("NearestPinId")]
        public int Id { get; set; }

        [Column("MemberShipId")]
        public int MembershipId { get; set; }

        [Column("MatchId")]
        public int MatchId { get; set; }

        [Column("VgcNo")]
        public int VgcNo { get; set; }

        [Column("Firstname")]
        public string Firstname { get; set; }

        [Column("Lastname")]
        public string Lastname { get; set; }

        [Column("PinName")]
        public string PinName { get; set; }

        [Column("CourseName")]
        public string CourseName { get; set; }

        [Column("DistanceInCM")]
        public int DistanceInCM { get; set; }
    }

    public class Par3Result
    {
        public Par3Result()
        {

        }
    }
    }
