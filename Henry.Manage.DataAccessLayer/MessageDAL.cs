using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class MessageDAL
    {
        public static DataTable GetList(Message condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [Message] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Message condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "M_CreateTime DESC";
            var data = new 
            {
                TableName = "Message",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,M_ID,M_Type,M_Read,M_Status,M_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(Message condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.M_ID <= 0)
            {
                cmdString = "INSERT INTO [Message](M_Type,M_Read,M_Status) VALUES(@M_Type,@M_Read,@M_Status)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@M_Type",condition.M_Type);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@M_Read",condition.M_Read);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@M_Status",condition.M_Status);
                paramer[3].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [Message] SET M_Type=@M_Type,M_Read=@M_Read,M_Status=@M_Status WHERE M_ID=@M_ID";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@M_ID",condition.M_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@M_Type",condition.M_Type);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@M_Read",condition.M_Read);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@M_Status",condition.M_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Message] SET M_Status=@M_Status WHERE M_ID=@M_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@M_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@M_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


