using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;
using Caddie.Data.Dto;

namespace Caddie.Test
{
    [TestClass]
    public class MatchPlayTest
    {
        [TestMethod]
        public void TestMatchPlayGetall()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.MatchPlayTeamList(DateTime.Now.Year).ToList();

                Assert.IsNotNull(x);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestMatchPlayTeamUdate()
        {
            try
            {
                int season = DateTime.Now.Year;
                Repository rep = new Repository();
                rep.MatchPlayDeleteSeasonTeams(season);

                var x = new Data.Dto.LeagueTeam() {
                    LeagueId = 1,
                    VgcNo = 1701
                };
                rep.MatchPlayTeamUpdate(x);
                var list = rep.MatchPlayTeamList(season).ToList();
                Assert.AreEqual(list.Count, 1);

                x = new Data.Dto.LeagueTeam()
                {
                    LeagueId = 3,
                    VgcNo = 1701,
                    VgcNoPartner = 1698
                };
                rep.MatchPlayTeamUpdate(x);
                list = rep.MatchPlayTeamList(season).ToList();
                Assert.AreEqual(list.Count, 2);

                x = new Data.Dto.LeagueTeam()
                {
                    LeagueId = 3,
                    VgcNo = 1698,
                    VgcNoPartner = 1701
                };
                rep.MatchPlayTeamUpdate(x);
                list = rep.MatchPlayTeamList(season).ToList();
                Assert.AreEqual(list.Count, 2);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestMatchPlayMatchUdate()
        {
            try
            {
                int leagueId = 1;
                int season = DateTime.Now.Year;
                Repository rep = new Repository();
                var teams = rep.MatchPlayTeamList(season).Where(m => m.LeagueId == leagueId).ToList();

                LeagueTeam t1 = teams[1];
                LeagueTeam t2 = teams[3];

                var x = new Data.Dto.LeagueMatch()
                {
                    LeagueId = leagueId,
                    Playround = 2,
                    LeagueTeamId1 = t1.LeagueTeamId,
                    LeagueTeamId2 = t2.LeagueTeamId
                };
                rep.MatchPlayMatchUpdate(x);
                //var list = .MatchPlayTeamList(season).ToList();

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
