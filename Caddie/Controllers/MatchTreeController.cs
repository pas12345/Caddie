using Caddie.Models;
using System.Collections.Generic;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "matchTree", "Matcher")]
    public class MatchTreeController : TreeBaseController
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
            var mainRoute = "caddie/matchTree";

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            if (id > 0)
            {
                TreeNode item = this.CreateTreeNode("-1", 
                    id.ToString(),
                    queryStrings, 
                    "Øvrige resultater", 
                    "icon-calendar", 
                    false,
                    string.Format("{0}/edit/{1}", mainRoute, id));
                nodes.Add(item);
            }
            else
            {
                int season = MyRepository.Season;
                IEnumerable<Data.Dto.Match> dtos = base.MyRepository.GetSeasonMatches();
                List<ItemModel> matches= AutoMapper.Mapper.Map<List<ItemModel>>(dtos);

                foreach (ItemModel match in matches)
                {
                    TreeNode item = this.CreateTreeNode(match.Id.ToString(),
                        id.ToString(),
                        queryStrings,
                        match.StringValue,
                        "icon-bullhorn",
                        false,
                        string.Format("{0}/edit/{1}", mainRoute, match.Id));
                    nodes.Add(item);
                }
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

            //If the node is the root node (top of tree)
            if (id == Constants.System.Root.ToInvariantString())
            {
                menu.Items.Add<ActionNew>("Opret match");

                //Add in refresh action
                menu.Items.Add<RefreshNode, ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                    , ActionRefresh.Instance.Alias)));
                //menu.Items.Add<RefreshNode, ActionRefresh>(
                //    ApplicationContext.Services.TextService.Localize(ActionRefresh.Instance.Alias, CultureInfo.DefaultThreadCurrentUICulture));
            }

            return menu;
        }
    }
}