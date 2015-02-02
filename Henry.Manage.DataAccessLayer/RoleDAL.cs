using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class RoleDAL
    {
        public static DataTable GetList(Role condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [Role] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Role condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "R_CreateTime DESC";
            var data = new 
            {
                TableName = "Role",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,R_ID,R_Name,R_Status,R_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(Role condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.R_ID <= 0)
            {
                cmdString = "INSERT INTO [Role](R_Name,R_Status) VALUES(@R_Name,@R_Status)";
                paramer = new SqlParameter[2];
                paramer[1] = new SqlParameter("@R_Name",condition.R_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 20;  
                paramer[2] = new SqlParameter("@R_Status",condition.R_Status);
                paramer[2].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [Role] SET R_Name=@R_Name,R_Status=@R_Status WHERE R_ID=@R_ID";
                paramer = new SqlParameter[3];
                paramer[0] = new SqlParameter("@R_ID",condition.R_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@R_Name",condition.R_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 20;  
                paramer[2] = new SqlParameter("@R_Status",condition.R_Status);
                paramer[2].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Role] SET R_Status=@R_Status WHERE R_ID=@R_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@R_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@R_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


