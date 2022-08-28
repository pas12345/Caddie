using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;
using Caddie.Data.Dto;
using System.Collections.Generic;

namespace Caddie.Test
{
    [TestClass]
    public class ResultTest
    {
        [TestMethod]
        public void TestGetMatchResults()
        {
            try
            {
                // lav så den test slagspil, ,hallington og sf - med og uden maxrows
                Repository rep = new Repository();
                int matchId = 458, maxA = 13;

                IEnumerable<ResultMatch> dtos = rep.GetMatchResultWinners(matchId, maxA);

                Assert.IsNotNull(dtos);

                var list = dtos.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetMatchResultPlayers()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetMatchResults(startDate, startDate.AddYears(1));

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetMemberResults()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetMatchResults(startDate, startDate.AddYears(1), 1701);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0, "No results for member");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetLastResult()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetLastResult();

                Assert.IsNotNull(x);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetMemberResultForRegistration()
        {
            try
            {
                Repository rep = new Repository();
                Match m = rep.GetNextMatch(rep.Season-1);
                if (m == null)
                    m = rep.GetNextMatch(rep.Season);
                Assert.IsNotNull(m);

                m = rep.GetLastMatch();
                Assert.IsNotNull(m);

                List<ResultMatch> rs = rep.GetMatchResultListForRegistration(m.Id);
                Assert.IsNotNull(rs);

                ResultMatch rx = rs.FirstOrDefault();
                Assert.IsNotNull(rx);
                Assert.IsTrue(rx.MatchformId > 0, "Matchformid not found in list");

                ResultMatch r = rep.GetMatchResultForRegistration(m.Id, 1701);
                Assert.IsNotNull(r);
                Assert.IsTrue(r.MatchformId > 0, "Matchformid not found");

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        #region Ranking
        [TestMethod]
        public void TestGetBruttoRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetBruttoRanking(startDate, startDate.AddYears(1), 5);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetHallingtonRanking()
        {
            try
            {
                DateTime startDate = new DateTime(DateTime.Now.Year-1, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetHallingtonRanking(startDate, startDate.AddYears(1), 2);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetNettoRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetNettoRanking(startDate, startDate.AddYears(1), 5);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetPutsRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetPutsRanking(startDate, startDate.AddYears(1), 5);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetDamstahlRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetDamstahlRanking(startDate, startDate.AddYears(1));

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestGetStablefordRanking()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetStablefordRanking(this.Season);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TesGetShootoutRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetShootoutRanking(startDate, startDate.AddYears(1));

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetOverallRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season, 1, 1);
                Repository rep = new Repository();
                var x = rep.GetOverallRanking(startDate, startDate.AddYears(1));

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetNearestPinRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season+1, 1, 1);
                //if (startDate.AddMonths(4) > DateTime.Now)
                //    startDate = startDate.AddYears(-1);
                Repository rep = new Repository();
                var x = rep.GetNearestPinRanking(startDate, startDate.AddYears(1));

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestCompetitionRanking()
        {
            try
            {
                DateTime startDate = new DateTime(this.Season + 1, 1, 1);
                //if (startDate.AddMonths(4) > DateTime.Now)
                //    startDate = startDate.AddYears(-1);
                Repository rep = new Repository();
                var x = rep.GetCompetitionResult(493);

                Assert.IsNotNull(x);

                var list = x.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetCompetitionResult()
        {
            try
            {
                // lav så den test slagspil, ,hallington og sf - med og uden maxrows
                Repository rep = new Repository();
                int matchId = 493;

                IEnumerable<CompetitionResult> dtos = rep.GetCompetitionResults(matchId);

                Assert.IsNotNull(dtos);

                var list = dtos.ToList();
                Assert.IsTrue(list.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        #endregion

        private int Season
        {
            get {
                return (DateTime.Now.Month > 5) ? DateTime.Now.Year : DateTime.Now.Year - 1;
            }
        }
    }
}
