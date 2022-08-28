using System;
using System.Collections.Generic;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Caddie.Models;
using System.Linq;

namespace Caddie.Controllers
{
    [PluginController("Caddie")]
    [Umbraco.Web.Trees.Tree("caddie", "playerTree", "Medlemmer", "icon-users")]
    public class PlayerTreeController : TreeBaseController
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
            var mainRoute = "caddie/playerTree";

            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            List<KeyValuePair<int, string>> groups = new List<KeyValuePair<int, string>>() {
                    new KeyValuePair<int, string>(0, "A-B"),
                    new KeyValuePair<int, string>(1, "C-G"),
                    new KeyValuePair<int, string>(2, "H-I"),
                    new KeyValuePair<int, string>(3, "-J-"),
                    new KeyValuePair<int, string>(4, "-K-"),
                    new KeyValuePair<int, string>(5, "L-O"),
                    new KeyValuePair<int, string>(6, "P-R"),
                    new KeyValuePair<int, string>(7, "S-")
                };

            try
            {
                if (id == -1)
                {
                    foreach (var item in groups)
                    {
                        TreeNode groupNode = this.CreateTreeNode((item.Key - 10).ToString(),
                            id.ToString(),
                            queryStrings,
                            groups[item.Key].Value,
                            "icon-users-alt",
                            true,
                            null);
                        nodes.Add(groupNode);
                    }
                }
                else if(id < -1)
                {
                    int season = base.MyRepository.Season;
                    IEnumerable<Data.Dto.Player> dtos = MyRepository.GetMemberNames(season);
                    List<PlayerModel> list = AutoMapper.Mapper.Map<IEnumerable<PlayerModel>>(dtos).ToList();

                    int groupId = 10 + id;
                    list.Where(x => x.NameGroup == groupId).ForEach(p =>
                    {
                        TreeNode playerNode = CreateTreeNode(p.MemberShipId.ToString(),
                            id.ToString(),
                            queryStrings,
                            p.Name,
                            "icon-user",
                            false,
                            string.Format("{0}/edit/{1}", mainRoute, p.MemberShipId));
                        nodes.Add(playerNode);
                    });
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("PlayerTreeController::TreeNodeCollection failed: {0}", ex.Message);
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
                menu.Items.Add<ActionNew>("Opret");

                var m = new MenuItem("playerHcpIndex", "Opdater spiller Hcp");
                m.Icon = "enter";
                menu.Items.Add(m);

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