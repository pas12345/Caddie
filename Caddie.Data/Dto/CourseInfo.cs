using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.vCourse")]
    [PrimaryKey("CourseDeailId", AutoIncrement = true)]
    [ExplicitColumns]

    public class CourseInfo
    {
        [Column("ClubId")]
        public int ClubId { get; set; }
        [Column("ClubName")]
        public string ClubName { get; set; }
        [Column("CourseId")]
        public int CourseId { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("Slope")]
        public int Slope { get; set; }
        [Column("CourseRating")]
        public decimal CourseRating { get; set; }
        [Column("Par")]
        public int Par { get; set; }
        [Column("Tee")]
        public string Tee { get; set; }
        [Column("CourseTeeId")]
        public int CourseTeeId { get; set; }
        [Column("CourseDetailId")]
        public int CourseDetailId { get; set; }
        [Column("IsMale")]
        public bool IsMale { get; set; }
    }
}
