using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSReports.Components;

namespace Caddie.Test
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void TestMatchResultReport()
        {
            string binPath = System.AppDomain.CurrentDomain.BaseDirectory;
            MatchResult r = new MatchResult("vgcmsSqlServer", binPath);
            byte[] v = r.GeneratePDF(493);

            Assert.IsNotNull(v);
            System.IO.File.WriteAllBytes(@"c:\temp\MatchResult.pdf", v);
        }
        [TestMethod]
        public void TestTourReport()
        {
            string binPath = System.AppDomain.CurrentDomain.BaseDirectory;
            TourRegistration r = new TourRegistration("vgcmsSqlServer", binPath);
            byte[] v = r.GeneratePDF(78, 0);

            Assert.IsNotNull(v);
            System.IO.File.WriteAllBytes(@"c:\temp\Tour.pdf", v);
        }
        [TestMethod]
        public void TestRegistrationReport()
        {
            string binPath = System.AppDomain.CurrentDomain.BaseDirectory;
            MatchRegistration r = new MatchRegistration("vgcmsSqlServer", binPath);
            byte[] v = r.GeneratePDF(507, 0);

            Assert.IsNotNull(v);
            System.IO.File.WriteAllBytes(@"c:\temp\Registration.pdf", v);
        }
    }
}
