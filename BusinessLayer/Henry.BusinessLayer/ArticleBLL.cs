using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Henry.BusinessLayer
{
    public class ArticleBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<Article> GetList(Article condition)
        {
            return ArticleDAL.GetList(condition).ToEntity<Article>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<Article> GetListWithPage(Article condition)
        {
            int totalcount=0;
            var result= ArticleDAL.GetListWithPage(condition,out totalcount).ToEntity<Article>();;
            return new PageParamer<Article> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static Article GetSingleOrDefault(int keyval)
        {
            var condition = new Article  { A_ID = keyval };
            return  ArticleDAL.GetList(condition).ToEntity<Article>().FirstOrDefault();
        }

        #endregion

        
    }
}

