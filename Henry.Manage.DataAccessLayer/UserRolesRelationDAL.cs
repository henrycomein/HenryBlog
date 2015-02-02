using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class UserRolesRelationDAL
    {
        public static DataTable GetList(UserRolesRelation condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [UserRolesRelation] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(UserRolesRelation condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "UR_CreateTime DESC";
            var data = new 
            {
                TableName = "UserRolesRelation",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,UR_ID,UR_UserID,UR_RoleID,UR_Status,UR_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(UserRolesRelation condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.UR_ID <= 0)
            {
                cmdString = "INSERT INTO [UserRolesRelation](UR_UserID,UR_RoleID,UR_Status) VALUES(@UR_UserID,@UR_RoleID,@UR_Status)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@UR_UserID",condition.UR_UserID);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@UR_RoleID",condition.UR_RoleID);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@UR_Status",condition.UR_Status);
                paramer[3].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [UserRolesRelation] SET UR_UserID=@UR_UserID,UR_RoleID=@UR_RoleID,UR_Status=@UR_Status WHERE UR_ID=@UR_ID";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@UR_ID",condition.UR_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@UR_UserID",condition.UR_UserID);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@UR_RoleID",condition.UR_RoleID);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@UR_Status",condition.UR_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [UserRolesRelation] SET UR_Status=@UR_Status WHERE UR_ID=@UR_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@UR_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@UR_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


