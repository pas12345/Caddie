using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;

namespace Caddie.Test
{
    [TestClass]
    public class MatchTest
    {
        [TestMethod]
        public void TestGetMatch()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetMatch(447);

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetMatchDates()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetMatchDates(DateTime.Now.AddMonths(-6), DateTime.Now);

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetLastMatch()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetLastMatch();

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TesGetMatchesBefore()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetMatchesBefore(DateTime.Now);

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TesGetSeasonMatches()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetSeasonMatches();

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetNextMatch()
        {
            try
            {
                Repository rep = new Repository();

                var x = rep.GetNextMatch(rep.Season);
                if (x == null)
                    x = rep.GetNextMatch(rep.Season + 1);
                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
