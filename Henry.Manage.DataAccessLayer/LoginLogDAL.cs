using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class LoginLogDAL
    {
        public static DataTable GetList(LoginLog condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [LoginLog] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(LoginLog condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "L_Browser DESC";
            var data = new 
            {
                TableName = "LoginLog",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,L_ID,L_UserID,L_LoginTime,L_IpAddress,L_Browser,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(LoginLog condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.L_ID <= 0)
            {
                cmdString = "INSERT INTO [LoginLog](L_UserID,L_LoginTime,L_IpAddress) VALUES(@L_UserID,@L_LoginTime,@L_IpAddress)";
                paramer = new SqlParameter[3];
                paramer[0].ParameterName = "L_UserID";
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[0].Value = condition.L_UserID;  
                paramer[1].ParameterName = "L_LoginTime";
                paramer[1].SqlDbType = SqlDbType.DateTime;
                paramer[1].Value = condition.L_LoginTime;  
                paramer[2].ParameterName = "L_IpAddress";
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Value = condition.L_IpAddress;  
                paramer[3].Size = 15;  
            }
            else
            {
                cmdString ="UPDATE [LoginLog] SET L_UserID=@L_UserID,L_LoginTime=@L_LoginTime,L_IpAddress=@L_IpAddress WHERE L_ID=@L_ID";
                paramer = new SqlParameter[4];
                paramer[0].ParameterName = "L_ID";
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[0].Value = condition.L_ID; 
                paramer[1].ParameterName = "L_UserID";
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[1].Value = condition.L_UserID; 
                paramer[2].ParameterName = "L_LoginTime";
                paramer[2].SqlDbType = SqlDbType.DateTime;
                paramer[2].Value = condition.L_LoginTime; 
                paramer[3].ParameterName = "L_IpAddress";
                paramer[3].SqlDbType = SqlDbType.NVarChar;
                paramer[3].Value = condition.L_IpAddress; 
                paramer[3].Size = 15;  
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [LoginLog] SET L_IpAddress=@L_IpAddress WHERE L_ID=@L_ID";
            var paramer=new SqlParameter[2];
            paramer[0].ParameterName="L_ID";
            paramer[0].SqlDbType= SqlDbType.Int;
            paramer[0].Value=id;

            paramer[1].ParameterName="L_IpAddress";
            paramer[1].SqlDbType= SqlDbType.Int;
            paramer[1].Value=id;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


