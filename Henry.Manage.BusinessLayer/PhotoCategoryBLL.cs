using Henry.Manage.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Manage.BusinessLayer
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
            var condition = new PhotoCategory  { PC_ID = keyval };
            return  PhotoCategoryDAL.GetList(condition).ToEntity<PhotoCategory>().FirstOrDefault();
        }

        #endregion

        #region handle data

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="info">data infomation</param>
        /// <returns>success or fail</returns>
        public static bool Add(PhotoCategory info)
        {
            return PhotoCategoryDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(PhotoCategory info)
        {
            return PhotoCategoryDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// Disable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Disable(int id)
        {
            return PhotoCategoryDAL.UpdateStatus(id, 0); 
        }

        /// <summary>
        /// Enable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Enable(int id)
        {
            return PhotoCategoryDAL.UpdateStatus(id, 1);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return PhotoCategoryDAL.UpdateStatus(id, 2);
        }
        
         /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="photoid">照片ID</param>
        /// <param name="categoryid">类别ID</param>
        /// <returns></returns>
        public static bool SetCover(int photoid,int categoryid)
        {
            return PhotoCategoryDAL.SetCover(photoid,categoryid);
        }
        #endregion
    }
}

