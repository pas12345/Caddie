using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;

namespace Caddie.Test
{
    [TestClass]
    public class TourTest
    {
        [TestMethod]
        public void TestGetSeasonTours()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetSeasonTours(DateTime.Now.Year-1, DateTime.Now);

                Assert.IsNotNull(x);
                Assert.IsTrue(x.ToList().Count > 0, "No tours");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetTourPlayers()
        {
            try
            {
                Repository rep = new Repository();

                var tours = rep.GetSeasonTours(DateTime.Now.Year - 1, DateTime.Now.AddYears(-1));
                Assert.IsNotNull(tours);

                Caddie.Data.Dto.Tour tour = tours.ToList().FirstOrDefault();

                var players = rep.GetTourPlayers(tour.Id);
                Assert.IsNotNull(players);

                Caddie.Data.Dto.TourPlayer t = players.ToList().FirstOrDefault();
                Assert.IsNotNull(t, "No players on tour found");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetTour()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetSeasonTours(DateTime.Now.Year - 1, DateTime.Now.AddYears(-1));

                Assert.IsNotNull(x);
                Caddie.Data.Dto.Tour t = x.ToList().FirstOrDefault();

                Caddie.Data.Dto.Tour t2 = rep.GetTour(t.Id);

                Assert.IsNotNull(t2);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        [TestMethod]
        public void TestIsTourPlayerRegistrered()
        {
            try
            {
                int vgcno = 1701;
                Repository rep = new Repository();

                var tours = rep.GetSeasonTours(DateTime.Now.Year - 1, DateTime.Now.AddYears(-1));
                Assert.IsNotNull(tours);

                Caddie.Data.Dto.Tour tour = tours.ToList().FirstOrDefault();

                bool res = rep.TourPlayerIsRegistered(tour.Id, vgcno);

                rep.TourChangeRegistration(tour.Id, vgcno, "X");
                bool res2 = rep.TourPlayerIsRegistered(tour.Id, vgcno);
                Assert.IsTrue(res != res2, "Change of registration failed");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        [TestMethod]
        public void TestTourUpdate()
        {
            try
            {
                Repository rep = new Repository();

                Caddie.Data.Dto.Tour tour = new Caddie.Data.Dto.Tour
                {
                    Id = 0,
                    TourDate = DateTime.Now.AddMonths(2),
                    Description = "Test tur 2",
                    LastRegistrationDate = DateTime.Now.AddMonths(1),
                    OpenForSignUp = true,
                    MaxNoOfMembers = 100,
                    UrlDescription = 0,
                    NoOfMembers = 0,
                    MatchId = -1,
                    SponsorLogoId = 0
                };

                tour = rep.UpdateTour(tour);

                Assert.IsTrue(tour.Id > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}
