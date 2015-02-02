using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Henry.BusinessLayer
{
    public class MenusBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<Menus> GetList(Menus condition)
        {
            return MenusDAL.GetList(condition).ToEntity<Menus>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<Menus> GetListWithPage(Menus condition)
        {
            int totalcount=0;
            var result= MenusDAL.GetListWithPage(condition,out totalcount).ToEntity<Menus>();;
            return new PageParamer<Menus> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static Menus GetSingleOrDefault(int keyval)
        {
            var condition = new Menus  { M_ID = keyval };
            return  MenusDAL.GetList(condition).ToEntity<Menus>().FirstOrDefault();
        }

        #endregion

        
    }
}

