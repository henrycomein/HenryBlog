using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
namespace Henry.DataAccessLayer
{
    public class ArticleDAL
    {
        public static DataTable GetList(Article condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            if (condition.A_CategoryID > 0) sqlCondition.AppendFormat("AND A_CategoryID={0}", condition.A_CategoryID);
            var queryString = "SELECT * FROM Article " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Article condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "A_CreateTime DESC";
            var data = new 
            {
                TableName = "Article",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,A_ID,A_Title,A_CategoryID,A_Content,A_IsTop,A_Sort,A_Status,A_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
    }
}


