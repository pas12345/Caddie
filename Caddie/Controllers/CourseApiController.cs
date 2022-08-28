using Caddie.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class CourseApiController : ApiBaseController
    {

        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult GetClub(int clubId)
        {
            try
            {
                Data.Dto.Club dto = MyRepository.GetClub(clubId);
                if (dto == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<ItemModel>(dto));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }


        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetCourseList(int clubId)
        {
            try
            {
                IEnumerable<Data.Dto.ListEntry> dtos = MyRepository.GetCourseList(clubId);
                if (dtos == null)
                    return NotFound();
                var lst = AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<CourseModel>))]
        public IHttpActionResult GetClubCourses(int clubId)
        {
            try
            {
                IEnumerable<Data.Dto.CourseInfo> dtos = MyRepository.GetClubCourses(clubId);
                if (dtos == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<IEnumerable<CourseModel>>(dtos));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(CourseModel))]
        public IHttpActionResult GetCourse(int courseId)
        {
            try
            {
                Data.Dto.CourseInfo dto = MyRepository.GetCourse(courseId);
                if (dto == null)
                    return NotFound();
                return Ok(AutoMapper.Mapper.Map<CourseModel>(dto));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(IEnumerable<ItemModel>))]
        public IHttpActionResult GetCourseItems()
        {
            try
            {
                IEnumerable<Data.Dto.CourseInfo> dtos = MyRepository.GetCourses();
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
        public IHttpActionResult GetTees()
        {
            try
            {
                IEnumerable<Data.Dto.ListEntry> dtos = MyRepository.GetTees();
                if (dtos == null)
                    return NotFound();
                var lst = AutoMapper.Mapper.Map<IEnumerable<ItemModel>>(dtos);
                return Ok(lst);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(CourseModel))]
        public IHttpActionResult UpdateClub([FromBody]ItemModel item)
        {
            if (item == null)
            {
                return BadRequest("Club name cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.ListEntry>(item);
                MyRepository.UpdateClub(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult UpdateCourse([FromBody]ItemModel model)
        {
            if (model == null)
            {
                return BadRequest("CourseDetail cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.ListEntry>(model);
                MyRepository.UpdateCourse(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [ResponseType(typeof(CourseModel))]
        public IHttpActionResult UpdateCourseDetail([FromBody]CourseModel model)
        {
            if (model == null)
            {
                return BadRequest("CourseDetail cannot be null");
            }
            try
            {
                var dto = AutoMapper.Mapper.Map<Data.Dto.CourseInfo>(model);
                MyRepository.UpdateCourseDetail(dto);
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
