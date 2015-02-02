using Henry.DataAccessLayer;
using Henry.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Henry.Common;

namespace Henry.BusinessLayer
{
    public class ArticleCategoryBLL
    {
        #region GetData

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
        /// get top categories
        /// </summary>
        /// <returns></returns>
        public static List<ArticleCategory> GetTopCategories()
        {
            return ArticleCategoryDAL.GetList(new ArticleCategory() { AC_ParentID = 0, AC_ShowFront = 1 }).ToEntity<ArticleCategory>();
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
            var condition = new ArticleCategory { AC_ID = keyval };
            return ArticleCategoryDAL.GetList(condition).ToEntity<ArticleCategory>().FirstOrDefault();
        }
        public static PageParamer<ArticleCategory> GetShowItemsByCode(string key, int pageindex, int pagesize)
        {
            int total=0;
            var result = ArticleCategoryDAL.GetShowItemsByCode(key,pageindex,pagesize,out total).ToEntity<ArticleCategory>();
            return new PageParamer<ArticleCategory> { Items = result, TotalCount = total };
        }
        /// <summary>
        /// 获取大类别下面所有小类别（除去ShowList=1的）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<ArticleCategory> GetItemsByCode(string key)
        {
            string cacheKey="category_" + key;
            List<ArticleCategory> categories;
            var objData = CacheHelper.Get(cacheKey);
            if (objData == null)
            { 
                categories=ArticleCategoryDAL.GetItemsByCode(key).ToEntity<ArticleCategory>();
                CacheHelper.AddWithDependency(cacheKey, categories, key);
            }
            else
            {
                categories=(List<ArticleCategory>)objData;
            }
            return categories;
        }
        #endregion

    }
}

