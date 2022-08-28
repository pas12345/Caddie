using System;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "reportTree", "Udskrifter", sortOrder:7)]
    public class ReportTreeController : TreeBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        /// 
        protected override TreeNodeCollection GetTreeNodes(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Main Route
            var mainRoute = "caddie/reportTree";

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            try
            {
                if (id > 0)
                {
                    TreeNode item = this.CreateTreeNode("-1",
                        id.ToString(),
                        queryStrings,
                        "Udskrifter",
                        "icon-print",
                        false,
                        null);
                    nodes.Add(item);
                }
                else
                {
                    TreeNode item = this.CreateTreeNode("1",
                        "1",
                        queryStrings,
                        "Tilmelding",
                        "icon-share",
                        false,
                        string.Format("{0}/registration/1", mainRoute));
                    nodes.Add(item);

                    item = this.CreateTreeNode("2",
                        "2",
                        queryStrings,
                        "Resultater",
                        "icon-crown-alt",
                        false,
                        string.Format("{0}/result/1", mainRoute));
                    nodes.Add(item);

                    item = this.CreateTreeNode("3",
                        "3",
                        queryStrings,
                        "Udflugter",
                        "icon-plane",
                        false,
                        string.Format("{0}/tour/1", mainRoute));
                    nodes.Add(item);
                }

            }
            catch (Exception ex)
            {
                Log.ErrorFormat("ReportTreeController::TreeNodeCollection failed: {0}", ex.Message);
            }
            return nodes;
        }

        /// <summary>
        /// Get menu/s for nodes in tree
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override MenuItemCollection GetMenuForNode(string id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            int i = 0;
            if (Int32.TryParse(id, out i))
            {
                if (i > 0)
                {
                    var m = new MenuItem("editMatch", "Opret match");
                    m.Icon = "enter";
                    menu.Items.Add(m);
                }
            }

            //If the node is the root node (top of tree)
            if (id == Constants.System.Root.ToInvariantString())
            {
                //Add in refresh action
                menu.Items.Add<RefreshNode, ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                    , ActionRefresh.Instance.Alias)));
                //menu.Items.Add<RefreshNode, ActionRefresh>(
                //    ApplicationContext.Services.TextService.Localize(ActionRefresh.Instance.Alias);
            }

            return menu;
        }
    }
}