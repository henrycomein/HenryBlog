using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.BusinessLayer
{
    public class PhotoBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<Photo> GetList(Photo condition)
        {
            return PhotoDAL.GetList(condition).ToEntity<Photo>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<Photo> GetListWithPage(Photo condition)
        {
            int totalcount=0;
            var result= PhotoDAL.GetListWithPage(condition,out totalcount).ToEntity<Photo>();;
            return new PageParamer<Photo> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static Photo GetSingleOrDefault(int keyval)
        {
            var condition = new Photo  { P_ID = keyval };
            return  PhotoDAL.GetList(condition).ToEntity<Photo>().FirstOrDefault();
        }

        #endregion

        
    }
}

