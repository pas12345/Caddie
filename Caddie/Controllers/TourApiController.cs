using Caddie.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class TourApiController : ApiBaseController
    {
        [ResponseType(typeof(IEnumerable<TourModel>))]
        public IHttpActionResult GetTourList(int season)
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

        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetTourItems()
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.Tour> tours = MyRepository.GetSeasonTours(base.MyRepository.Season, DateTime.Now);
                if (tours == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(tours));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetAllTourItems()
        {
            try
            {
                IEnumerable<Caddie.Data.Dto.Tour> tours = MyRepository.GetSeasonTours(base.MyRepository.Season, new DateTime(base.MyRepository.Season, 1, 1));
                if (tours == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(tours));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(TourModel))]
        public IHttpActionResult GetTour(int tourId)
        {
            try
            {
                Data.Dto.Tour dto = MyRepository.GetTour(tourId);
                if (dto == null)
                    return NotFound();
                var model = AutoMapper.Mapper.Map<TourModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }


        [ResponseType(typeof(TourRegistrationModel))]
        public IHttpActionResult GetRegistration(int tourId, int vgcNo)
        {
            try
            {
                var tour = MyRepository.GetTour(tourId);
                bool b = MyRepository.TourPlayerIsRegistered(tourId, vgcNo);
                TourRegistrationModel model = new TourRegistrationModel()
                {
                    VgcNo = vgcNo,
                    IsRegistered = b, 
                    TourId = tourId,
                    Open = (tour.NoOfMembers < tour.MaxNoOfMembers)
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(TourRegistrationModel))]
        public IHttpActionResult TourRegistration([FromBody]TourRegistrationModel model)
        {
            try
            {
                MyRepository.TourChangeRegistration(model.TourId, model.VgcNo, "admin");
                model.IsRegistered = MyRepository.TourPlayerIsRegistered(model.TourId, model.VgcNo);
                return Ok(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        // POST: api/Tour
        [ResponseType(typeof(TourModel))]
        public IHttpActionResult SaveTour([FromBody]TourModel model)
        {
            if (model == null)
            {
                return BadRequest("Tour cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.Tour>(model);
                dto = MyRepository.UpdateTour(dto);
                if (dto == null)
                {
                    return NotFound();
                }
                model = AutoMapper.Mapper.Map<TourModel>(dto);
                return Created<TourModel>(Request.RequestUri + model.Id.ToString(), model);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

    }
}
