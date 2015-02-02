using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class WebLogsDAL
    {
        public static DataTable GetList(WebLogs condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [WebLogs] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(WebLogs condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "CreateTime DESC";
            var data = new 
            {
                TableName = "WebLogs",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,id,LogLevel,ErrorMsg,Logger,CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(WebLogs condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.id <= 0)
            {
                cmdString = "INSERT INTO [WebLogs](LogLevel,ErrorMsg,Logger) VALUES(@LogLevel,@ErrorMsg,@Logger)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@LogLevel",condition.LogLevel);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@ErrorMsg",condition.ErrorMsg);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = -1;  
                paramer[3] = new SqlParameter("@Logger",condition.Logger);
                paramer[3].SqlDbType = SqlDbType.NVarChar; 
                paramer[3].Size = 50;  
            }
            else
            {
                cmdString ="UPDATE [WebLogs] SET LogLevel=@LogLevel,ErrorMsg=@ErrorMsg,Logger=@Logger WHERE id=@id";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@id",condition.id);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@LogLevel",condition.LogLevel);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@ErrorMsg",condition.ErrorMsg);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = -1;  
                paramer[3] = new SqlParameter("@Logger",condition.Logger);
                paramer[3].SqlDbType = SqlDbType.NVarChar;
                paramer[3].Size = 50;  
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [WebLogs] SET Logger=@Logger WHERE id=@id";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@id", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@Logger", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


