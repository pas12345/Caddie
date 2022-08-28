using Caddie.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class ResultApiController : ApiBaseController
    {

        #region ResulRegstration
        [ResponseType(typeof(List<ResultRegistrationModel>))]
        public IHttpActionResult GetMatchResultListForRegistration(int matchId)
        {
            try
            {
                List<Caddie.Data.Dto.ResultMatch> results = MyRepository.GetMatchResultListForRegistration(matchId);
                if (results == null)
                    return NotFound();

                IEnumerable<ResultRegistrationModel> model =
                    AutoMapper.Mapper.Map<List<ResultRegistrationModel>>(results);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        // GET: api/results/match/matchId/vgcNo
        [ResponseType(typeof(ResultRegistrationModel))]
        public IHttpActionResult GetMatchResultForRegistration(int matchId, int vgcNo)
        {
            try
            {
                ResultRegistrationModel model = new ResultRegistrationModel();
                Caddie.Data.Dto.ResultMatch result = MyRepository.GetMatchResultForRegistration(matchId, vgcNo);
                if (result != null)
                {
                    if (result.MatchformId < 1)
                        return BadRequest("MatchformId not found");

                    model = AutoMapper.Mapper.Map<ResultRegistrationModel>(result);
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(List<ResultRegistrationModel>))]
        public IHttpActionResult GetMatchResults(int matchId)
        {
            try
            {
                List<Data.Dto.ResultMatch> dto = MyRepository.GetMatchResults(matchId);
                if (dto == null)
                    return NotFound();
                List<ResultRegistrationModel> res = AutoMapper.Mapper.Map<List<ResultRegistrationModel>>(dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(ResultRegistrationModel))]
        public IHttpActionResult GetMatchResult(int matchResultId)
        {
            try
            {
                Data.Dto.ResultMatch dto = MyRepository.GetMatchResult(matchResultId);
                if (dto == null)
                    return NotFound();
                ResultRegistrationModel res = AutoMapper.Mapper.Map<ResultRegistrationModel>(dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        // POST: api/Results
        [HttpPost]
        public IHttpActionResult DeleteMatchResult([FromBody]int id)
        {
            try
            {
                MyRepository.DeleteMatchResult(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        // PUT: api/Results/5
        [ResponseType(typeof(ResultRegistrationModel))]
        public IHttpActionResult Put(int id, [FromBody]ResultRegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest("Result cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.ResultMatch>(model);
                dto = MyRepository.UpdateMatchResult(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<ResultRegistrationModel>(dto);
                return Created<ResultRegistrationModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }
        // POST: api/Results
        [HttpPost]
        [ResponseType(typeof(ResultRegistrationModel))]
        public IHttpActionResult SaveResult([FromBody]ResultRegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest("Result cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.ResultMatch>(model);
                dto = MyRepository.UpdateMatchResult(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<ResultRegistrationModel>(dto);
                return Created<ResultRegistrationModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);

            }
        }
        #endregion

        #region Results

        #endregion

        #region Competition
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetCompetitions()
        {
            try
            {
                IEnumerable<Data.Dto.ListEntry> dtos = MyRepository.GetCompetitions();
                if (dtos == null)
                    return Ok(new List<ItemModel>());
                List<ItemModel> res =
                    AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos).ToList();

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        [ResponseType(typeof(CompetitionResultModel))]
        public IHttpActionResult GetCompetitionResultById(int id)
        {
            try
            {
                Data.Dto.CompetitionResult dto = MyRepository.GetCompetitionResultById(id);
                if (dto == null)
                    return Ok(new CompetitionResultModel());
                CompetitionResultModel res = AutoMapper.Mapper.Map<CompetitionResultModel>(dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(CompetitionResultModel))]
        public IHttpActionResult GetCompetitionResult(int matchId, int competitionId)
        {
            try
            {
                Data.Dto.CompetitionResult dto = MyRepository.GetCompetitionResult(matchId, competitionId);
                if (dto == null)
                    return Ok(new CompetitionResultModel()
                        { CompetitionId = competitionId,
                          MatchId = matchId
                        });
                CompetitionResultModel res = AutoMapper.Mapper.Map<CompetitionResultModel>(dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
        // POST: api/Results
        [HttpPost]
        [ResponseType(typeof(CompetitionResultModel))]
        public IHttpActionResult SaveCompetitionResult([FromBody]CompetitionResultModel model)
        {
            if (model == null)
            {
                return BadRequest("Result cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.CompetitionResult>(model);
                dto = MyRepository.UpdateCompetitionResult(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<CompetitionResultModel>(dto);
                return Created<CompetitionResultModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }


        #endregion

        #region Nearest pin

        [ResponseType(typeof(NearestPinResultModel))]
        public IHttpActionResult GetNearestPinResult(int id)
        {
            try
            {
                Data.Dto.NearestPinResult dto = MyRepository.GetNearestPinResult(id);
                if (dto == null)
                    return NotFound();
                NearestPinResultModel res = AutoMapper.Mapper.Map<NearestPinResultModel>(dto);

                return Ok(res);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }



        // POST: api/Results
        [HttpPost]
        [ResponseType(typeof(NearestPinResultModel))]
        public IHttpActionResult SavePinResult([FromBody]NearestPinResultModel model)
        {
            if (model == null)
            {
                return BadRequest("Result cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.NearestPinResult>(model);
                dto = MyRepository.UpdateNearestPinResult(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<NearestPinResultModel>(dto);
                return Created<NearestPinResultModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }


        #endregion

        // POST: api/Results
        [HttpPost]
        public IHttpActionResult SettleMatch([FromBody]string matchId)
        {
            try
            {
                int i = 0;
                Int32.TryParse(matchId, out i);
                Data.Dto.Match match = MyRepository.GetMatch(i);
                if (match == null)
                    return NotFound();

                switch (match.MatchformId)
                {
                    case 1:
                        MyRepository.MatchResultSettleByStroke(i);
                        Log.InfoFormat("MatchResultSettleByStroke({0})", i);
                        break;
                    case 3:
                        MyRepository.MatchResultSettleByHallington(i);
                        Log.InfoFormat("MatchResultSettleByHallington({0})", i);
                        break;

                    default:
                        MyRepository.MatchResultSettleByPoints(i);
                        Log.InfoFormat("MatchResultSettleByPoints({0})", i);
                        break;
                }
                MyRepository.MatchResultSetDamstahlPoints(i);
                Log.InfoFormat("MatchResultSetDamstahlPoints({0})", i);
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
