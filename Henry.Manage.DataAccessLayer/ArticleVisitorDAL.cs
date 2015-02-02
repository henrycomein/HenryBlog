using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class ArticleVisitorDAL
    {
        public static DataTable GetList(ArticleVisitor condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [ArticleVisitor] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(ArticleVisitor condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "AV_CreateTime DESC";
            var data = new 
            {
                TableName = "ArticleVisitor",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,AV_ID,AV_IpAddress,AV_Status,AV_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(ArticleVisitor condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.AV_ID <= 0)
            {
                cmdString = "INSERT INTO [ArticleVisitor](AV_IpAddress,AV_Status) VALUES(@AV_IpAddress,@AV_Status)";
                paramer = new SqlParameter[2];
                paramer[1] = new SqlParameter("@AV_IpAddress",condition.AV_IpAddress);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 15;  
                paramer[2] = new SqlParameter("@AV_Status",condition.AV_Status);
                paramer[2].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [ArticleVisitor] SET AV_IpAddress=@AV_IpAddress,AV_Status=@AV_Status WHERE AV_ID=@AV_ID";
                paramer = new SqlParameter[3];
                paramer[0] = new SqlParameter("@AV_ID",condition.AV_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@AV_IpAddress",condition.AV_IpAddress);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 15;  
                paramer[2] = new SqlParameter("@AV_Status",condition.AV_Status);
                paramer[2].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [ArticleVisitor] SET AV_Status=@AV_Status WHERE AV_ID=@AV_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@AV_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@AV_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


