using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caddie.Data;

namespace Caddie.Test
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void TestGetMemberNames()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetMemberNames(DateTime.Now.Year-1);

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
        public void TestGetPlayerInfo()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetPlayer(1701);

                Assert.IsNotNull(x);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestGetPlayer()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetPlayer(1701, DateTime.Now.Year - 1);

                Assert.IsNotNull(x);

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void TestUpdatePlayer()
        {
            int vgcNo = 9999;
            Repository rep = new Repository();
            try
            {
                rep.DeletePlayer(vgcNo);
                Data.Dto.Player dto = new Data.Dto.Player()
                {
                    VgcNo = 9999,
                    Firstname = "Anders",
                    Lastname = "And",
                    City = "Andeby",
                    HcpIndex = 12.2M
                };
                rep.UpdatePlayer(dto);

                var x = rep.GetPlayer(vgcNo);
                Assert.IsNotNull(x);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally {
                rep.DeletePlayer(vgcNo);
            }
        }

        public void CreatePlayer()
        {
            int vgcNo = 9999;
            Repository rep = new Repository();
            try
            {
                rep.DeletePlayer(vgcNo);
                Data.Dto.Player dto = new Data.Dto.Player()
                {
                    VgcNo = 9999,
                    Firstname = "Anders",
                    Lastname = "And",
                    City = "Andeby"
                };
                rep.UpdatePlayer(dto);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        [TestMethod]
        public void TestUpdateMembership()
        {
            int vgcNo = 9999;
            Repository rep = new Repository();
            try
            {
                CreatePlayer();
                int i = rep.UpdateMembership(vgcNo, DateTime.Now.Year);
                Assert.IsTrue(i > 0, "MembershipId should be create then 0");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            finally
            {
                rep.DeleteMembership( vgcNo, DateTime.Now.Year);
                rep.DeletePlayer(vgcNo);
            }
        }
        [TestMethod]
        public void TestGetNonMemberNames()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetNonMemberNames(DateTime.Now.Year);

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
        public void TestGetAllNonMemberNames()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetAllNonMemberNames(DateTime.Now.Year - 1);

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
        public void TestGetMemberships()
        {
            try
            {
                Repository rep = new Repository();
                var x = rep.GetMemberships(DateTime.Now.Year - 1, -1);

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
        public void TestUpdateHcp()
        {
            try
            {
                Repository rep = new Repository();
                int i = rep.UpdateHcp(1701, 5);

                i = rep.UpdateHcp(1701, 5.2M);
                Assert.IsFalse(i == 0);

                i = rep.UpdateHcp(1701, 5.2M);
                Assert.IsTrue(i == 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        //        public bool UpdatePlayer(Data.Dto.Player p)
    }
}
