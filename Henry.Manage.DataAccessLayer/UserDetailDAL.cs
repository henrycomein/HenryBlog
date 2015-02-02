using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class UserDetailDAL
    {
        public static DataTable GetList(UserDetail condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [UserDetail] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(UserDetail condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "UD_Email DESC";
            var data = new 
            {
                TableName = "UserDetail",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,UD_ID,UD_UserID,UD_NickName,UD_Avatar,UD_Email,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(UserDetail condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.UD_ID <= 0)
            {
                cmdString = "INSERT INTO [UserDetail](UD_UserID,UD_NickName,UD_Avatar) VALUES(@UD_UserID,@UD_NickName,@UD_Avatar)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@UD_UserID",condition.UD_UserID);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@UD_NickName",condition.UD_NickName);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = 20;  
                paramer[3] = new SqlParameter("@UD_Avatar",condition.UD_Avatar);
                paramer[3].SqlDbType = SqlDbType.NVarChar; 
                paramer[3].Size = 100;  
            }
            else
            {
                cmdString ="UPDATE [UserDetail] SET UD_UserID=@UD_UserID,UD_NickName=@UD_NickName,UD_Avatar=@UD_Avatar WHERE UD_ID=@UD_ID";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@UD_ID",condition.UD_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@UD_UserID",condition.UD_UserID);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@UD_NickName",condition.UD_NickName);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 20;  
                paramer[3] = new SqlParameter("@UD_Avatar",condition.UD_Avatar);
                paramer[3].SqlDbType = SqlDbType.NVarChar;
                paramer[3].Size = 100;  
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [UserDetail] SET UD_Avatar=@UD_Avatar WHERE UD_ID=@UD_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@UD_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@UD_Avatar", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


