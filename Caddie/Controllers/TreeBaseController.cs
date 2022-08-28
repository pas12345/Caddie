using System;
using System.Reflection;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;
using Umbraco.Core;
using umbraco.BusinessLogic.Actions;
using System.Globalization;

namespace Caddie.Controllers
{
    public class TreeBaseController : TreeController
    {
        protected static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region MyRepository
        private Caddie.Data.Repository repos;
        protected Caddie.Data.Repository MyRepository
        {
            get
            {
                if (null == repos)
                    repos = new Caddie.Data.Repository();
                return repos;
            }
        }

        #endregion


        #region overrides
        protected override Umbraco.Web.Models.Trees.MenuItemCollection GetMenuForNode(string id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            MenuItemCollection menu = new MenuItemCollection();
            int i = 0;
            if (!int.TryParse(id, out i))
            {
                Log.Error("GetMenuForNode failed to parse id: " + id);
                return menu;
            }
            return GetMenuForNode(i, queryStrings);
        }

        /// <summary>
        /// Get menu/s for nodes in tree
        /// </summary>
        /// <param name="id"></param>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        protected virtual Umbraco.Web.Models.Trees.MenuItemCollection GetMenuForNode(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }


        protected virtual Umbraco.Web.Models.Trees.MenuItemCollection GetMenuForNode(int[] ids, int levels, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }

        protected override Umbraco.Web.Models.Trees.TreeNodeCollection GetTreeNodes(string id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            TreeNodeCollection nodes = new TreeNodeCollection();
            char[] delimiterChars = { ';' };
            string[] ids = id.Split(delimiterChars);
            int levels = ids.GetLength(0);
            int[] idInts = new int[levels];
            for (int i = 0; i < levels; i++)
            {
                if (!int.TryParse(ids[i], out idInts[i]))
                {
                    Log.Error("GetTreeNodes failed to parse id: " + id);
                    return nodes;
                }
            }

            if (levels > 1)
                return GetTreeNodes(idInts, levels, queryStrings);
            return GetTreeNodes(idInts[0], queryStrings);
        }

        protected virtual TreeNodeCollection GetTreeNodes(int id, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            throw new NotImplementedException();
        }

        protected virtual TreeNodeCollection GetTreeNodes(int[] ids, int levels, System.Net.Http.Formatting.FormDataCollection queryStrings)
        {
            //Nodes that we will return
            throw new NotImplementedException();
        }
        #endregion
    }

}