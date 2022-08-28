using System;
using System.Collections.Generic;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Caddie.Models;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "tourTree", "Udflugter")]
    public class TourTreeController : TreeBaseController
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
            var mainRoute = "caddie/tourTree";

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            try
            {
                if (id > 0)
                {
                    TreeNode item = this.CreateTreeNode("-1",
                        id.ToString(),
                        queryStrings,
                        "Udflugter",
                        "icon-plane",
                        false,
                        string.Format("{0}/edit/{1}", mainRoute, id));
                    nodes.Add(item);
                }
                else
                {
                    int season = base.MyRepository.Season;
                    IEnumerable<Data.Dto.Tour> dtos = base.MyRepository.GetSeasonTours(season, new DateTime(season, 1, 1));
                    List<ItemModel> tours = AutoMapper.Mapper.Map<List<ItemModel>>(dtos);

                    foreach (ItemModel tour in tours)
                    {
                        TreeNode item = this.CreateTreeNode(tour.Id.ToString(),
                            id.ToString(),
                            queryStrings,
                            tour.StringValue,
                            "icon-plane",
                            false,
                            string.Format("{0}/edit/{1}", mainRoute, tour.Id));
                        nodes.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.ErrorFormat("TourTreeController::TreeNodeCollection failed: {0}", ex.Message);
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

            try
            {
                int i = 0;
                if (Int32.TryParse(id, out i))
                {
                    if (i > 0)
                    {
                        var m = new MenuItem("tourRegistration", "Tilmeld");
                        m.Icon = "plane";
                        menu.Items.Add(m);
                    }
                }
                Log.InfoFormat("TourTreeController::GetMenuForNode menu items: {0}", menu.Items.Count);

                //If the node is the root node (top of tree)
                if (id == Constants.System.Root.ToInvariantString())
                {
                    menu.Items.Add<ActionNew>("Opret tur");
                    //Add in refresh action
                    menu.Items.Add<RefreshNode,
                        ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                                        , ActionRefresh.Instance.Alias)));
                }
            }
            catch (Exception e)
            {
                Log.ErrorFormat("TourTreeController::GetMenuForNode failed: {0}", e.Message);
            }
            return menu;
        }
    }
}