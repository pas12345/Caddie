using Caddie.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class MatchplayApiController : ApiBaseController
    {
        [ResponseType(typeof(MatchplayMatchModel))]
        public IHttpActionResult GetMatch(int matchId)
        {
            try
            {
                Data.Dto.LeagueMatch match = MyRepository.GetMatchplayMatch(matchId);
                if (match == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<MatchplayMatchModel>(match));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<MatchplayMatchModel>))]
        public IHttpActionResult GetMatches(int leagueId)
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.LeagueMatch> dtos = MyRepository.GetMatchplayMatchList(leagueId);
                if (dtos == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<IEnumerable<MatchplayMatchModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(MatchplayMatchModel))]
        public IHttpActionResult SaveMatchplayResult([FromBody]MatchplayMatchModel model)
        {
            if (model == null)
            {
                return BadRequest("Match cannot be null");
            }
            if (string.IsNullOrEmpty(model.ResultText))
            { 
                if (model.MatchResult < 3)
                    return BadRequest("Resultatet er ikke angivet");
            }
            else { 
                if (model.MatchResult == 3)
                    return BadRequest("Vinder ikke angivet");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.LeagueMatch>(model);
                MyRepository.UpdateMatchplayResult(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<MatchplayTeamModel>))]
        public IHttpActionResult GetTeams(int leagueId)
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.LeagueTeam> dtos = MyRepository.GetMatchplayTeams(leagueId);
                if (dtos == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<IEnumerable<MatchplayTeamModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(MatchplayTeamModel))]
        public IHttpActionResult GetPlayerTeam(int vgcNo)
        {
            try
            {
                Caddie.Data.Dto.LeagueTeam dto = MyRepository.MatchplayTeamExists(vgcNo);
                if (dto == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<MatchplayTeamModel>(dto));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(MatchplayTeamModel))]
        public IHttpActionResult GetParTeam(int vgcNo, int vgcNoPartner)
        {
            try
            {
                Caddie.Data.Dto.LeagueTeam dto = MyRepository.MatchplayTeamExists(vgcNo, vgcNoPartner);
                if (dto == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<MatchplayTeamModel>(dto));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(MatchplayTeamModel))]
        public IHttpActionResult GetTeam(int id)
        {
            try
            {
                Caddie.Data.Dto.LeagueTeam dto = MyRepository.MatchplayGetTeam(id);
                if (dto == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(AutoMapper.Mapper.Map<MatchplayTeamModel>(dto));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(MatchplayMatchModel))]
        public IHttpActionResult UpdateTeam([FromBody]MatchplayTeamModel model)
        {
            if (model == null)
            {
                return BadRequest("Match cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.LeagueTeam>(model);
                MyRepository.MatchPlayTeamUpdate(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteTeam(int id)
        {
            try
            {
                MyRepository.MatchplayDeleteTeam(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(MatchplayMatchModel))]
        public IHttpActionResult UpdateMatch([FromBody]MatchplayMatchModel model)
        {
            if (model == null)
            {
                return BadRequest("Match cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.LeagueMatch>(model);
                MyRepository.MatchPlayMatchUpdate(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

    }
}
