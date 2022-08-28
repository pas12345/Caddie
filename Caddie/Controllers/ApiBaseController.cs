using log4net;
using System.Reflection;
using Umbraco.Web.WebApi;

namespace Caddie.Controllers
{
    public class ApiBaseController : UmbracoAuthorizedApiController
    {
        private Caddie.Data.Repository repo = null;
        public Caddie.Data.Repository MyRepository {
            get
            {
                if (repo == null)
                    repo = new Caddie.Data.Repository();
                return repo;
            }
        }

        #region Log
        protected static readonly ILog Log =
        LogManager.GetLogger(
            MethodBase.GetCurrentMethod().DeclaringType
        );
        #endregion
    }
}
