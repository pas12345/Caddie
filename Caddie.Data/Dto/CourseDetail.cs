using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.vCourseInfo")]
    [ExplicitColumns]
    public class CourseDetail
    {
        [Column("CourseDetailId")]
        public int CourseDetailId { get; set; }
        [Column("ClubName")]
        public string ClubName { get; set; }
        [Column("ClubId")]
        public int ClubId { get; set; }
        [Column("CourseName")]
        public string CourseName { get; set; }
        [Column("CourseId")]
        public int CourseId { get; set; }
        [Column("Par")]
        public int Par { get; set; }
        [Column("IsMale")]
        public bool IsMale { get; set; }
        [Column("TeeColour")]
        public string TeeColour { get; set; }
        [Column("CourseTeeId")]
        public int CourseTeeId { get; set; }
        [Column("Slope")]
        public int Slope { get; set; }
        [Column("CourseRating")]
        public decimal CourseRating { get; set; }
    }
}
