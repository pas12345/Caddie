using Caddie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class PlayerApiController : ApiBaseController
    {
        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult GetMemberList()
        {
            try
            {
                int season = base.MyRepository.Season;
                IEnumerable<Caddie.Data.Dto.Membership> dtos = MyRepository.GetMemberships(season, -1);
                if (dtos == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<PlayerModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult GetNonMemberList()
        {
            try
            {
                int season = base.MyRepository.Season;
                IEnumerable<Data.Dto.Player> players = MyRepository.GetNonMemberNames(season);
                if (players == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<PlayerModel>>(players));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetMemberNames(int season)
        {
            try
            {
                IEnumerable<Data.Dto.Player> dtos = MyRepository.GetMemberNames(season);
                if (dtos == null)
                    return NotFound();

                List<ItemModel> list = AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos).ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(PlayerModel))]
        public IHttpActionResult GetMember(int id)
        {
            try
            {
                Data.Dto.Player dto = MyRepository.GetMember(id);
                if (dto == null)
                    return NotFound();
                var model = AutoMapper.Mapper.Map<PlayerModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(PlayerModel))]
        public IHttpActionResult GetPlayer(int vgcNo)
        {
            try
            {
                Data.Dto.Player dto = MyRepository.GetPlayer(vgcNo);
                if (dto == null)
                    return NotFound();
                var model = AutoMapper.Mapper.Map<PlayerModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(PlayerModel))]
        public IHttpActionResult GetMemberOrPlayer(int id)
        {
            try
            {
                Data.Dto.Player dto = null;
                if (id < 0)
                    dto = MyRepository.GetPlayer(-1*id);
                else
                    dto = MyRepository.GetMember(id);
                if (dto == null)
                    return NotFound();

                var model = AutoMapper.Mapper.Map<PlayerModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        // POST: api/Results
        [ResponseType(typeof(PlayerModel))]
        public IHttpActionResult Save([FromBody]PlayerModel model)
        {
            if (model == null || model.VgcNo < 1)
            {
                return BadRequest("Player cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.Player>(model);
                MyRepository.UpdatePlayer(dto);
                if (dto.Season > 0)
                {
                    int membershipId = MyRepository.UpdateMembership(dto.VgcNo, dto.Season);
                    dto = MyRepository.GetMember(membershipId);
                }
                else
                    dto = MyRepository.GetPlayer(dto.VgcNo);

                if (dto == null)
                    return NotFound();

                model = AutoMapper.Mapper.Map<PlayerModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        // POST: api/Results
        [ResponseType(typeof(PlayerModel))]
        public IHttpActionResult EnsureExists([FromBody]int vgcNo)
        {
            try
            {
                if (vgcNo == 0)
                    return BadRequest("Vgc nr. cannot be 0");

                var dto = new Data.Dto.Player() { VgcNo = vgcNo, Firstname = "-" };
                MyRepository.UpdatePlayer(dto);

                PlayerModel model = AutoMapper.Mapper.Map<PlayerModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }
        // GET: api/UpdateHcp
        public IHttpActionResult GetUpdatedHcp()
        {
            try
            {
                Log.Info("UpdateHcp called");

                Dictionary<int, decimal> hcps = new Dictionary<int, decimal>();
                GolfboxManager mgr = new GolfboxManager();
                hcps = mgr.GetHandicaps(MyRepository.WsAccount,
                                        MyRepository.WsUsername,
                                        MyRepository.WsPassword,
                                        new System.Guid(MyRepository.WsGroupGuid));
                Log.InfoFormat(string.Format("UpdateHandicap() updated {0} members", hcps.Count));

                int season = base.MyRepository.Season;
                IEnumerable<Caddie.Data.Dto.Membership> dtos = MyRepository.GetMemberships(season, -1); 
                if (dtos == null)
                    return NotFound();
                List < Caddie.Data.Dto.Membership > members = dtos.ToList();

                int count = 0;
                foreach (Data.Dto.Membership m in members)
                {
                    if (hcps.ContainsKey(m.VgcNo))
                    {
                        decimal hcpIndex = hcps[m.VgcNo] / 10000M;
                        int i = MyRepository.UpdateHcp(m.VgcNo, hcpIndex);
                        if (i > 0)
                        {
                            Log.InfoFormat("UpdateHandicap {0}:  {1,1} -> {2,1}", 
                                m.VgcNo, m.HcpIndex, hcpIndex);
                            count++;
                        }
                    }
                    else
                        Log.ErrorFormat("UpdateHandicap could not find hcp for {0}, {1}", m.VgcNo, m.Firstname);
                }
                return Ok(count);
            }
            catch (Exception ex)
            {
                Log.Error("UpdateHandicap() failed: " + ex.ToString());
                return InternalServerError(ex);
            }
        }


    }
}
