using System;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;


namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "matchplayTree", "Hulspil")]
    public class MatchplayTreeController : TreeBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override TreeNodeCollection GetTreeNodes(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Main Route
            var mainRoute = "caddie/matchplayTree";

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            if (id < 0)
            {
                TreeNode item = this.CreateTreeNode("1", 
                    "-1",
                    queryStrings, 
                    "A rækken", 
                    "icon-bulleted-list", 
                    true,
                    null);
                nodes.Add(item);
                item = this.CreateTreeNode("2",
                    "-1",
                    queryStrings,
                    "B rækken",
                    "icon-bulleted-list",
                    true,
                    null);
                nodes.Add(item);
                item = this.CreateTreeNode("3",
                    "-1",
                    queryStrings,
                    "Par",
                    "icon-bulleted-list",
                    true,
                    null);
                nodes.Add(item);
            }
            else
            {

                TreeNode item = this.CreateTreeNode((id + 10).ToString(),
                    id.ToString(),
                    queryStrings,
                    "Hold",
                    "icon-users-alt",
                    false,
                    string.Format("{0}/teamList/{1}", mainRoute, -1*id));
                nodes.Add(item);

                item = this.CreateTreeNode((id + 20).ToString(),
                    id.ToString(),
                    queryStrings,
                    "Matcher",
                    "icon-mindmap",
                    false,
                    string.Format("{0}/matchList/{1}", mainRoute, -1*id));

                nodes.Add(item);
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
                if (9 < i && i < 20)
                {
                    var m = new MenuItem("createTeam2", "Opret hold");
                    m.Icon = "icon-users-alt";
                    menu.Items.Add(m);
                }
                if (19 < i)
                {
                    var m = new MenuItem("createMatch", "Opret match");
                    m.Icon = "icon-mindmap";
                    menu.Items.Add(m);
                }
            }


            //If the node is the root node (top of tree)
            if (id == Constants.System.Root.ToInvariantString())
            {
                //Add in refresh action
                menu.Items.Add<RefreshNode, ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                    , ActionRefresh.Instance.Alias)));
            }

            return menu;
        }
    }
}