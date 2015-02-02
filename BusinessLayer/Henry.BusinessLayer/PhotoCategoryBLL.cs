using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.BusinessLayer
{
    public class PhotoCategoryBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<PhotoCategory> GetList(PhotoCategory condition)
        {
            return PhotoCategoryDAL.GetList(condition).ToEntity<PhotoCategory>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<PhotoCategory> GetListWithPage(PhotoCategory condition)
        {
            int totalcount=0;
            var result= PhotoCategoryDAL.GetListWithPage(condition,out totalcount).ToEntity<PhotoCategory>();;
            return new PageParamer<PhotoCategory> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static PhotoCategory GetSingleOrDefault(int keyval)
        {
            var condition = new PhotoCategory  { PC_ID = keyval,PC_Status=1,PC_Show=1 };
            return  PhotoCategoryDAL.GetList(condition).ToEntity<PhotoCategory>().FirstOrDefault();
        }

        #endregion

        
    }
}

