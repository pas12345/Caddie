using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    public class ReportApiController : ApiBaseController
    {
        [HttpGet]
        public HttpResponseMessage MatchRegistrationReport(int matchId)
        {
            try
            {
                Log.InfoFormat("MatchRegistrationReport({0})", matchId);

                var gen = new MSReports.Components.MatchRegistration("vgcmsSqlServer");
                Log.InfoFormat("MatchRegistrationReport - binPath: {0}", gen.BinPath);

                byte[] bytes = gen.GeneratePDF(matchId, 10);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = gen.ReportName + ".pdf"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage MatchResultReport(int matchId)
        {
            try
            {
                Log.InfoFormat("MatchResultReport({0})", matchId);

                var gen = new MSReports.Components.MatchResult("vgcmsSqlServer");
                Log.InfoFormat("MatchResultReport - binPath: {0}", gen.BinPath);

                byte[] bytes = gen.GeneratePDF(matchId);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = gen.ReportName + ".pdf"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage TourRegistrationReport(int tourId)
        {
            try
            {
                Log.InfoFormat("TourRegistrationReport({0})", tourId);

                var gen = new MSReports.Components.TourRegistration("vgcmsSqlServer");
                Log.InfoFormat("TourRegistrationReport - binPath: {0}", gen.BinPath);

                byte[] bytes = gen.GeneratePDF(tourId, 20);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(bytes)
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = gen.ReportName + ".pdf"
                };
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
