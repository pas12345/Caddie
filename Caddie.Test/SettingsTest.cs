using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;

namespace Caddie.Test
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void TestGetSeason()
        {
            try
            {
                Repository rep = new Repository();
                int season = rep.Season;

                Assert.IsTrue(season >= DateTime.Now.Year - 1, "Season not set correctly");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
