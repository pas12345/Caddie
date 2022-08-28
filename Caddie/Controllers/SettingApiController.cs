using System;
using System.Web.Http;
using System.Web.Http.Description;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class SettingApiController : ApiBaseController
    {
        [ResponseType(typeof(int))]
        public IHttpActionResult GetSeason()
        {
            try
            {
                int season = MyRepository.Season;
                return Ok(season);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(string))]
        public IHttpActionResult GetSetting(int id)
        {
            try
            {
                Data.Dto.Property p = MyRepository.GetSetting(id);
                if (p == null)
                    return NotFound();
                return Ok(p.Value);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return InternalServerError(ex);
            }
        }
    }
}
