using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.CourseId")]
    [PrimaryKey("CourseId", AutoIncrement = true)]
    [ExplicitColumns]

    public class Course
    {
        [Column("ClubId")]
        public int ClubId { get; set; }
        [Column("ClubName")]
        public string ClubName { get; set; }
        [Column("CourseId")]
        public int CourseId { get; set; }
        [Column("CourseText")]
        public string CourseText { get; set; }
    }
}
