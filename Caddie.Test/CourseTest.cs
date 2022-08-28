using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;

namespace Caddie.Test
{
    [TestClass]
    public class CourseTest
    {
        [TestMethod]
        public void TestGetCourseInfo()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetClubCourses(5);

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void GetCourses()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetCourses();
                var club = rep.GetClub(15);
                var lst = rep.GetCourseList(49);
                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestUpdateCourseDetail()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetClub(15);

                Data.Dto.CourseInfo backup = rep.GetClubCourses(15).Skip(2).FirstOrDefault();

                Data.Dto.CourseInfo dto = new Data.Dto.CourseInfo
                {
                    Par = backup.Par,
                    Slope = backup.Slope,
                    CourseRating = backup.CourseRating,
                    CourseId = backup.CourseId,
                    CourseTeeId = backup.CourseTeeId,
                    ClubId = backup.ClubId,
                    CourseDetailId = backup.CourseDetailId
                };

                dto.Par = 32;
                dto.Slope = 120;
                dto.CourseRating = 43.1M;

                var c = rep.UpdateCourseDetail(dto);
                Assert.IsNotNull(c);

                dto = rep.GetCourse(backup.CourseDetailId);
                Assert.AreEqual(dto.Par, 32);
                Assert.AreEqual(dto.Slope, 120);
                Assert.AreEqual(dto.CourseRating, 43.1M);

                rep.UpdateCourseDetail(backup);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}
