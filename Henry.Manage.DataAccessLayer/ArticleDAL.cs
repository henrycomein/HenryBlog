using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class ArticleDAL
    {
        public static DataTable GetList(Article condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE A_Status<>2 ");
            if (condition.A_ID > 0) sqlCondition.AppendFormat(" AND A_ID={0} ",condition.A_ID);
            var queryString = "SELECT *,dbo.GetFullCategoryName(A_CategoryID) as A_CategoryName FROM [Article] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Article condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "A_Sort ASC,A_CreateTime DESC";
            var data = new 
            {
                TableName = "Article",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,A_ID,A_Title,A_CategoryID,A_IsTop,A_Sort,A_Status,A_CreateTime,dbo.GetFullCategoryName(A_CategoryID) as A_CategoryName", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE A_Status<>2 ");
            if (!string.IsNullOrWhiteSpace(condition.A_Content)) sqlCondition.AppendFormat(" AND A_Content like N'%{0}%'", condition.A_Content.CheckSqlParamer());
            if (condition.A_CategoryID > 0) sqlCondition.AppendFormat(" AND A_CategoryID ={0}", condition.A_CategoryID);
            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(Article condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.A_ID <= 0)
            {
                cmdString = "INSERT INTO [Article](A_Title,A_CategoryID,A_Content,A_IsTop,A_Sort,A_Status) VALUES(@A_Title,@A_CategoryID,@A_Content,@A_IsTop,@A_Sort,@A_Status)";
                paramer = new SqlParameter[6];
                paramer[1] = new SqlParameter("@A_Title",condition.A_Title);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@A_CategoryID",condition.A_CategoryID);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@A_Content",condition.A_Content);
                paramer[3].SqlDbType = SqlDbType.NVarChar; 
                paramer[3].Size = -1;  
                paramer[4] = new SqlParameter("@A_IsTop",condition.A_IsTop);
                paramer[4].SqlDbType = SqlDbType.Int; 
                paramer[5] = new SqlParameter("@A_Sort",condition.A_Sort);
                paramer[5].SqlDbType = SqlDbType.Int; 
                paramer[6] = new SqlParameter("@A_Status",condition.A_Status);
                paramer[6].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [Article] SET A_Title=@A_Title,A_CategoryID=@A_CategoryID,A_Content=@A_Content,A_IsTop=@A_IsTop,A_Sort=@A_Sort,A_Status=@A_Status WHERE A_ID=@A_ID";
                paramer = new SqlParameter[7];
                paramer[0] = new SqlParameter("@A_ID",condition.A_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@A_Title",condition.A_Title);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@A_CategoryID",condition.A_CategoryID);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@A_Content",condition.A_Content);
                paramer[3].SqlDbType = SqlDbType.NVarChar;
                paramer[3].Size = -1;  
                paramer[4] = new SqlParameter("@A_IsTop",condition.A_IsTop);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@A_Sort",condition.A_Sort);
                paramer[5].SqlDbType = SqlDbType.Int;
                paramer[6] = new SqlParameter("@A_Status",condition.A_Status);
                paramer[6].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Article] SET A_Status=@A_Status WHERE A_ID=@A_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@A_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@A_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


