using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Henry.BusinessLayer
{
    public class BackgroundImageBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<BackgroundImage> GetList(BackgroundImage condition)
        {
            return BackgroundImageDAL.GetList(condition).ToEntity<BackgroundImage>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<BackgroundImage> GetListWithPage(BackgroundImage condition)
        {
            int totalcount=0;
            var result= BackgroundImageDAL.GetListWithPage(condition,out totalcount).ToEntity<BackgroundImage>();;
            return new PageParamer<BackgroundImage> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static BackgroundImage GetSingleOrDefault(int keyval)
        {
            var condition = new BackgroundImage  { BG_ID = keyval };
            return  BackgroundImageDAL.GetList(condition).ToEntity<BackgroundImage>().FirstOrDefault();
        }

        #endregion

        
    }
}

