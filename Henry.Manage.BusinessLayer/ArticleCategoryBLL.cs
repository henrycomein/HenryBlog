using Henry.Manage.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Manage.BusinessLayer
{
    public class ArticleCategoryBLL
    {
        #region GetData

        public static List<ArticleCategory> GetTree(int id)
        {
            return ArticleCategoryDAL.GetTree(id).ToEntity<ArticleCategory>();
        }
        public static List<ArticleCategory> GetTreeList()
        {
            return ArticleCategoryDAL.GetTreeList().ToEntity<ArticleCategory>();
        }
        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<ArticleCategory> GetList(ArticleCategory condition)
        {
            return ArticleCategoryDAL.GetList(condition).ToEntity<ArticleCategory>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<ArticleCategory> GetListWithPage(ArticleCategory condition)
        {
            int totalcount=0;
            if (condition.PageSize < 0) condition.PageSize = 10;
            if (condition.PageIndex < 1) condition.PageIndex = 1;
            var result= ArticleCategoryDAL.GetListWithPage(condition,out totalcount).ToEntity<ArticleCategory>();;
            return new PageParamer<ArticleCategory> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static ArticleCategory GetSingleOrDefault(int keyval)
        {
            var condition = new ArticleCategory  { AC_ID = keyval };
            return  ArticleCategoryDAL.GetList(condition).ToEntity<ArticleCategory>().FirstOrDefault();
        }

        #endregion

        #region handle data

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="info">data infomation</param>
        /// <returns>success or fail</returns>
        public static bool Add(ArticleCategory info)
        {
            return ArticleCategoryDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(ArticleCategory info)
         {
            return ArticleCategoryDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// Disable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Disable(int id)
        {
            return ArticleCategoryDAL.UpdateStatus(id, 0); 
        }

        /// <summary>
        /// Enable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Enable(int id)
        {
            return ArticleCategoryDAL.UpdateStatus(id, 1);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return ArticleCategoryDAL.UpdateStatus(id, 2);
        }
        
        #endregion
    }
}

