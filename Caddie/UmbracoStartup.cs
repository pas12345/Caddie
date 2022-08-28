using Caddie.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using umbraco.cms.businesslogic.packager;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Trees;
using Umbraco.Web.UI.JavaScript;

namespace Caddie
{
    public class UmbracoStartup : ApplicationEventHandler
    {
        private const string AppSettingKey = "CaddieStartupInstalled";

        /// <summary>
        /// Register Install & Uninstall Events
        /// </summary>
        /// <param name="umbracoApplication"></param>
        /// <param name="applicationContext"></param>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Check to see if appSetting CaddieStartupInstalled is true or even present
            var installAppSetting = WebConfigurationManager.AppSettings[AppSettingKey];

            if (string.IsNullOrEmpty(installAppSetting) || installAppSetting != true.ToString())
            {
                var install = new InstallHelpers();

                LogHelper.Debug<UmbracoStartup>(() => "Installing Caddie: ");

                //Check to see if language keys for section needs to be added
                install.AddTranslations();

                //Check to see if section needs to be added
                install.AddSection(applicationContext);

                //Add Section Dashboard XML
                //install.AddSectionDashboard();

                // Give current user access to the section


                // does not work after 7.0
                //ApplicationContext.Current.Services.UserService.AddSectionToAllUsers("caddie", new int[] { });

                //All done installing our custom stuff
                //As we only want this to run once - not every startup of Umbraco
                //var webConfig = WebConfigurationManager.OpenWebConfiguration("/");
                //webConfig.AppSettings.Settings.Add(AppSettingKey, true.ToString());
                //webConfig.Save();
            }

            //Add OLD Style Package Event
            InstalledPackage.BeforeDelete += InstalledPackage_BeforeDelete;

            //Add Tree Node Rendering Event - Used to check if user is admin to display settings node in tree
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;

            AutoMapperConfig.RegisterMappings();

            ServerVariablesParser.Parsing += ServerVariablesParser_Parsing;
        }

        void ServerVariablesParser_Parsing(object sender, Dictionary<string, object> e)
        {
            if (System.Web.HttpContext.Current == null)
                throw new InvalidOperationException("HttpContext is null");

            if (e.ContainsKey("caddie")) return;

            var urlHelper = new UrlHelper(
                new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));
            e.Add("caddie", new Dictionary<string, object>
            {
                {"SettingApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<SettingApiController>(controller => controller.GetSetting(0))},
                {"TourApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<TourApiController>(controller => controller.GetTourList(0))},
                {"CourseApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<CourseApiController>(controller => controller.GetCourse(0))},
                {"ReportApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<ReportApiController>(controller => controller.MatchResultReport(0))},
                {"PlayerApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<PlayerApiController>(controller => controller.GetMemberList())},
                {"MatchApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<MatchApiController>(controller => controller.GetLastMatch())},
                {"MatchplayApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<MatchplayApiController>(controller => controller.GetMatches(0))},
                {"ResultApiUrl", urlHelper.GetUmbracoApiServiceBaseUrl<ResultApiController>(controller => controller.GetMatchResultListForRegistration(0))}
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            //Get Current User
            Umbraco.Core.Models.Membership.IUser user = UmbracoContext.Current.Security.CurrentUser;
            //This will only run on the CaddieTree & if the user is NOT admin
            if (sender.TreeAlias == "ResultTree" && user.Id > 0)
            {
                //setting node to remove
                var settingNode = e.Nodes.SingleOrDefault(x => x.Id.ToString() == "settings");

                //Ensure we found the node
                if (settingNode != null)
                {
                    //Remove the settings node from the collection
                    e.Nodes.Remove(settingNode);
                }
            }
        }

        /// <summary>
        /// Uninstall Package - Before Delete (Old style events, no V6/V7 equivelant)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InstalledPackage_BeforeDelete(InstalledPackage sender, System.EventArgs e)
        {
            //Check which package is being uninstalled
            if (sender.Data.Name == "Caddie")
            {
                var uninstall = new UninstallHelpers();

                //Start Uninstall - clean up process...
                uninstall.RemoveSection();
                uninstall.RemoveSectionDashboard();

                //Remove AppSetting key when all done
                ConfigurationManager.AppSettings.Remove(AppSettingKey);
            }
        }
    }

}