using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class UserDAL
    {
        public static DataTable GetList(User condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE U_Status<>2 ");
            
            if (condition.U_ID > 0) sqlCondition.AppendFormat(" AND U_ID={0}", condition.U_ID);
            if (!string.IsNullOrWhiteSpace(condition.U_Account)) sqlCondition.AppendFormat(" AND U_Account='{0}' ", condition.U_Account.CheckSqlParamer());
var queryString = @"SELECT * FROM [User] INNER JOIN UserDetail ON U_ID=UD_UserID  " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(User condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "U_CreateTime DESC";
            var data = new 
            {
                TableName = "User",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,U_ID,U_Account,U_Password,U_Status,U_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static DataTable GetMenus(string userid)
        {
            var queryString = @"SELECT m.* FROM Backend_Menus m
INNER JOIN RoleMenusRelation ON M_ID=RM_MenuID
INNER JOIN UserRolesRelation ON RM_RoleID=UR_RoleID
WHERE M_Status=1 AND RM_Status=1 AND UR_Status=1 AND UR_UserID=" + userid;
            return MySqlHelper.ExecuteQueryList(queryString); 
        }
        public static bool AddOrUpdate(User condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.U_ID <= 0)
            {
                cmdString = "INSERT INTO [User](U_Account,U_Password,U_Status) VALUES(@U_Account,@U_Password,@U_Status)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@U_Account",condition.U_Account);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 20;  
                paramer[2] = new SqlParameter("@U_Password",condition.U_Password);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = 50;  
                paramer[3] = new SqlParameter("@U_Status",condition.U_Status);
                paramer[3].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [User] SET U_Account=@U_Account,U_Password=@U_Password,U_Status=@U_Status WHERE U_ID=@U_ID";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@U_ID",condition.U_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@U_Account",condition.U_Account);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 20;  
                paramer[2] = new SqlParameter("@U_Password",condition.U_Password);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 50;  
                paramer[3] = new SqlParameter("@U_Status",condition.U_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [User] SET U_Status=@U_Status WHERE U_ID=@U_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@U_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@U_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


