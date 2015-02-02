using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Henry.BusinessLayer
{
    public class ArticleVisitorBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<ArticleVisitor> GetList(ArticleVisitor condition)
        {
            return ArticleVisitorDAL.GetList(condition).ToEntity<ArticleVisitor>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<ArticleVisitor> GetListWithPage(ArticleVisitor condition)
        {
            int totalcount=0;
            var result= ArticleVisitorDAL.GetListWithPage(condition,out totalcount).ToEntity<ArticleVisitor>();;
            return new PageParamer<ArticleVisitor> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static ArticleVisitor GetSingleOrDefault(int keyval)
        {
            var condition = new ArticleVisitor  { AV_ID = keyval };
            return  ArticleVisitorDAL.GetList(condition).ToEntity<ArticleVisitor>().FirstOrDefault();
        }

        #endregion

        
    }
}

