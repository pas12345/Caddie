using System;

namespace Caddie.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public int ClubId { get; set; }
        public string ClubName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Slope { get; set; }
        public decimal CourseRating { get; set; }
        public int Par { get; set; }
        public string Tee { get; set; }
        public int CourseTeeId { get; set; }
        public bool IsMale { get; set; }

        public string Description
        {
            get {
                return string.Format("{0}{1} Tee: {2}", ClubName
                    , string.Equals(ClubName, CourseName, StringComparison.InvariantCultureIgnoreCase) ? "" : ", " + CourseName
                    , Tee);
            }
        }
        public override string ToString()
        {
            return Description;
        }
    }
}
