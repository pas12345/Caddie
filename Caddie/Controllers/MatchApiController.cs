using Caddie.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class MatchApiController : ApiBaseController
    {
        //[Route("api/match/lastMatch")]
        [ResponseType(typeof(MatchModel))]
        public IHttpActionResult GetLastMatch()
        {
            try
            {
                Data.Dto.Match match = MyRepository.GetLastMatch();
                if (match == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<MatchModel>(match));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        // GET: api/Matches/5
        //[Route("api/match/{matchId}")]
        [ResponseType(typeof(MatchModel))]
        public IHttpActionResult GetMatch(int matchId)
        {
            try
            {
                Data.Dto.Match match = MyRepository.GetMatch(matchId);
                if (match == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<MatchModel>(match));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetMatchesBefore()
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.ListDateTimeEntry> dtos = MyRepository.GetMatchesBefore(DateTime.Now);
                if (dtos == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetMatchesAfter()
        {
            try
            {
                int season = base.MyRepository.Season;
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                IEnumerable<Caddie.Data.Dto.ListDateTimeEntry> dtos = MyRepository.GetMatchesAfter(dt);
                if (dtos == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetMatchforms()
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.ListEntry> dtos = MyRepository.GetMatchforms();
                if (dtos == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(MatchModel))]
        public IHttpActionResult SaveMatch([FromBody]MatchModel model)
        {
            if (model == null)
            {
                return BadRequest("Match cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.Match>(model);
                dto = MyRepository.UpdateMatch(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<MatchModel>(dto);
                return Created<MatchModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

    }
}
