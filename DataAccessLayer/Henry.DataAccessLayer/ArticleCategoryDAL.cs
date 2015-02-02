using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Henry.Entity;
using System.Data;
namespace Henry.DataAccessLayer
{
    public class ArticleCategoryDAL
    {
        public static DataTable GetList(ArticleCategory condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            if (condition.AC_ID > 0) sqlCondition.AppendFormat(" AND AC_ID={0}", condition.AC_ID);
            if (condition.AC_ParentID > -1) sqlCondition.AppendFormat(" AND AC_ParentID={0}", condition.AC_ParentID);
            if (condition.AC_ShowFront > -1) sqlCondition.AppendFormat("AND AC_ShowFront={0}", condition.AC_ShowFront);
            if (condition.AC_Status > -1) sqlCondition.AppendFormat("AND AC_Status={0}", condition.AC_Status);
            var queryString = "SELECT AC_ID, AC_Name, AC_Code, AC_ParentID, AC_ShowFront, AC_Description, AC_Sort, AC_PicName, AC_ShowList, AC_IsComplete, AC_Status, AC_CreateTime FROM ArticleCategory " + sqlCondition + " Order By AC_Sort asc";
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(ArticleCategory condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "AC_Sort ASC,AC_CreateTime DESC";
            var data = new
            {
                TableName = "ArticleCategory",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,AC_ID, AC_Name, AC_Code, AC_ParentID, AC_ShowFront, AC_Description, AC_Sort, AC_PicName, AC_ShowList, AC_IsComplete, AC_Status, AC_CreateTime", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE AC_Status=1 AND AC_ShowFront=1 AND AC_ShowList=1");

            if (!string.IsNullOrEmpty(condition.AC_Code)) sqlCondition.AppendFormat(" AND EXISTS(SELECT 1 FROM ArticleCategory c1 WHERE c1.AC_Code=N'{0}' AND c1.AC_ID=c.AC_ParentID)", condition.AC_Code.CheckSqlParamer());
            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        public static DataTable GetShowItemsByCode(string key, int pageindex, int pagesize,out int total)
        {
            var data= MySqlHelper.ExecuteCustomQuery(string.Format("EXECUTE GetBelongCategoryList N'{0}',{1},{2}", key.CheckSqlParamer(),pageindex,pagesize));
            total = Convert.ToInt32(data.Tables[1].Rows[0][0]);
            return data.Tables[0];
        }
        public static DataTable GetItemsByCode(string key)
        {
            return  MySqlHelper.ExecuteQueryList(string.Format("EXECUTE GetBelongMenuCategory N'{0}'", key.CheckSqlParamer()));
        }

    }
}


