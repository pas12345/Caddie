using System;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Core.Services;
using Caddie.Models;
using System.Collections.Generic;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "courseTree", "Baner")]
    public class CourseTreeController : TreeBaseController
    {
        private const string mainRoute = "caddie/courseTree";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override TreeNodeCollection GetTreeNodes(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Main Route

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            if (id > 0)
            {
                TreeNode item = this.CreateTreeNode("-1", 
                    id.ToString(),
                    queryStrings, 
                    "Baner", 
                    "icon-calendar", 
                    false,
                    string.Format("{0}/edit/{1}", mainRoute, id));
                nodes.Add(item);
            }
            else
            {
                int season = base.MyRepository.Season;
                IEnumerable<Data.Dto.Club> dtos = base.MyRepository.GetClubs();
                List<ItemModel> clubs = AutoMapper.Mapper.Map<List<ItemModel>>(dtos);

                foreach (ItemModel club in clubs)
                {
                    TreeNode item = this.CreateTreeNode(club.Id.ToString(),
                        id.ToString(),
                        queryStrings,
                        club.StringValue,
                        "icon-home",
                        false,
                        null);
                        //string.Format("{0}/courseList/{1}", mainRoute, club.Id));
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
                //Add in refresh action
                menu.Items.Add<ActionNew>("Opret klub");

                //Add in refresh action
                menu.Items.Add<RefreshNode, ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                    , ActionRefresh.Instance.Alias)));
                //menu.Items.Add<RefreshNode, ActionRefresh>(
                //    ApplicationContext.Services.TextService.Localize(ActionRefresh.Instance.Alias, CultureInfo.DefaultThreadCurrentUICulture));
            }
            else
            {
                var x = new MenuItem("courseList", "Baner");
                x.NavigateToRoute( string.Format("{0}/courseList/{1}", mainRoute, id));

                menu.Items.Add(x);
            }
            return menu;
        }
    }
}
