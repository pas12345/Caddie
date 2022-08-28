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
    [Umbraco.Web.Trees.Tree("caddie", "resultTree", "Resultater")]
    public class ResultTreeController : TreeBaseController
    {
        private const string mainRoute = "caddie/resultTree";

        #region TreeNodes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override TreeNodeCollection GetTreeNodes(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Nodes that we will return
            if (id == Constants.System.Root)
                return GetMatchNodes(queryStrings);

            return GetGroupNodes(id, queryStrings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override TreeNodeCollection GetTreeNodes(int[] ids, int levels, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            if (ids[1] == 1)
                return GetPinResultNodes(ids[0], queryStrings);
            return GetCompetitionNodes(ids[0], queryStrings);
        }

        private TreeNodeCollection GetMatchNodes(System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Nodes that we will return
            var nodes = new TreeNodeCollection();

            int season = base.MyRepository.Season;
            List<Caddie.Data.Dto.ListDateTimeEntry> dtos =
                base.MyRepository.GetMatchesBefore(DateTime.Now);

            List<TimeItemModel> dates = AutoMapper.Mapper.Map<List<Caddie.Models.TimeItemModel>>(dtos);
            Log.InfoFormat("Found {0} matches", dates.Count);

            if (dates != null)
            {
                foreach (TimeItemModel entry in dates)
                {
                    TreeNode item = this.CreateTreeNode(entry.Id.ToString(),
                        "-1",
                        queryStrings,
                        entry.DateTimeValue.ToString("d MMM yyyy"),
                        "icon-calendar",
                        true,
                        string.Format("{0}/resultList/{1}", mainRoute, entry.Id));

                    item.AdditionalData["matchId"] = entry.Id;
                    AddQueryStringsToAdditionalData(item, queryStrings);
                    nodes.Add(item);
                }
            }
            return nodes;
        }

        private TreeNodeCollection GetGroupNodes(int matchId, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {

            var nodes = new TreeNodeCollection();
            TreeNode item = this.CreateTreeNode(string.Format("{0};1", -1*matchId),
                matchId.ToString(),
                queryStrings,
                "Nærmest hullet",
                "icon-tab-key",
                true,
                string.Format("{0}/PinResult/{1}", mainRoute, matchId));
            item.AdditionalData.Add("matchId", matchId);
            nodes.Add(item);
            AddQueryStringsToAdditionalData(item, queryStrings);

            item = this.CreateTreeNode(string.Format("{0};2", -1*matchId),
                matchId.ToString(),
                queryStrings,
                "Øvrige resultater",
                "icon-coins",
                true,
                string.Format("{0}/CompetitionResult/{1}", mainRoute, matchId));
            item.AdditionalData.Add("matchId", matchId);
            nodes.Add(item);
            AddQueryStringsToAdditionalData(item, queryStrings);

            return nodes;
        }

        private TreeNodeCollection GetCompetitionNodes(int matchId, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Nodes that we will return
            var nodes = new TreeNodeCollection();
            matchId = Math.Abs(matchId);
            //var ctrl = new StatusApiController();
            //int level = id.Count(c => c == '-');
            IEnumerable<Caddie.Data.Dto.CompetitionResult> dtos =
                base.MyRepository.GetCompetitionResults(matchId);

            List<CompetitionResultModel> res = AutoMapper.Mapper.Map<List<Caddie.Models.CompetitionResultModel>>(dtos);
            if (res != null)
            {
                foreach (CompetitionResultModel entry in res)
                {
                    TreeNode item = this.CreateTreeNode(string.Format("{0};2;{1}", -1*matchId, entry.Id),
                        "2",
                        queryStrings,
                        entry.CompetitionText,
                        "icon-activity",
                        false,
                        string.Format("{0}/editCompetitionResult/{1}", mainRoute, entry.Id));
                    nodes.Add(item);
                    AddQueryStringsToAdditionalData(item, queryStrings);
                }
            }
            return nodes;
        }

        private TreeNodeCollection GetPinResultNodes(int matchId, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();
            matchId = Math.Abs(matchId);
            IEnumerable<Caddie.Data.Dto.NearestPinResult> dtos =
                base.MyRepository.GetNearestPinResults(matchId);

            var n = this.GetRootNode(queryStrings);
            //var c = this.GetNodes(matchId.ToString(), queryStrings);
            List<NearestPinResultModel> res = AutoMapper.Mapper.Map<List<Caddie.Models.NearestPinResultModel>>(dtos);
            if (res != null)
            {
                foreach (NearestPinResultModel entry in res)
                {
                    TreeNode item = this.CreateTreeNode(string.Format("{0};1;{1}", -1 * matchId, entry.Id),
                        "2",
                        queryStrings,
                        entry.PinName,
                        "icon-layers-alt",
                        false,
                        string.Format("{0}/editPinResult/{1}", mainRoute, entry.Id));
                    nodes.Add(item);
                }
            }
            return nodes;
        }
        #endregion
        /// <summary>
        /// Get menu/s for nodes in tree
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected override MenuItemCollection GetMenuForNode(string id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            char[] delimiterChars = { ';' };
            string[] ids = id.Split(delimiterChars);
            int levels = ids.GetLength(0);
            int[] idInts = new int[levels];
            for (int i = 0; i < levels; i++)
            {
                if (!int.TryParse(ids[i], out idInts[i]))
                {
                    Log.Error("GetMenuForNode failed to parse id: " + id);
                    return menu;
                }
            }
            if (idInts[0] == Constants.System.Root)
            {
                menu.Items.Add<RefreshNode, 
                    ActionRefresh>(Services.TextService.Localize(string.Format("actions/{0}"
                        , ActionRefresh.Instance.Alias)));
                return menu;
            }
            if (levels == 1)
            {
                var m = new MenuItem("matchSettlement", "Afslut matchen");
                m.Icon = "enter";
                m.AdditionalData.Add("matchId", idInts[0]);
                menu.Items.Add(m);

                m = new MenuItem("PinResult", "Nærmest flaget");
                m.AdditionalData.Add("matchId", idInts[0]);
                m.Icon = "enter";
                menu.Items.Add(m);

                m = new MenuItem("CompetitionResult", "Øvrige resultater");
                m.AdditionalData.Add("matchId", idInts[0]);
                m.Icon = "enter";
                menu.Items.Add(m);
            }
            if (levels == 2)
            {
                menu.Items.Add<ActionRefresh>("Genindlæs");
            }
            if (levels == 3)
            {
                menu.Items.Add<ActionDelete>("Slet");
                menu.Items[0].AdditionalData.Add("matchId", idInts[1]);
            }
            return menu;
        }

    }
}