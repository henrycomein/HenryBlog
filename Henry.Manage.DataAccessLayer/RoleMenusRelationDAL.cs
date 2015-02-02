using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class RoleMenusRelationDAL
    {
        public static DataTable GetList(RoleMenusRelation condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [RoleMenusRelation] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(RoleMenusRelation condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "RM_CreateTime DESC";
            var data = new 
            {
                TableName = "RoleMenusRelation",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,RM_ID,RM_RoleID,RM_MenuID,RM_Status,RM_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(RoleMenusRelation condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.RM_ID <= 0)
            {
                cmdString = "INSERT INTO [RoleMenusRelation](RM_RoleID,RM_MenuID,RM_Status) VALUES(@RM_RoleID,@RM_MenuID,@RM_Status)";
                paramer = new SqlParameter[3];
                paramer[1] = new SqlParameter("@RM_RoleID",condition.RM_RoleID);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@RM_MenuID",condition.RM_MenuID);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@RM_Status",condition.RM_Status);
                paramer[3].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [RoleMenusRelation] SET RM_RoleID=@RM_RoleID,RM_MenuID=@RM_MenuID,RM_Status=@RM_Status WHERE RM_ID=@RM_ID";
                paramer = new SqlParameter[4];
                paramer[0] = new SqlParameter("@RM_ID",condition.RM_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@RM_RoleID",condition.RM_RoleID);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@RM_MenuID",condition.RM_MenuID);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@RM_Status",condition.RM_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [RoleMenusRelation] SET RM_Status=@RM_Status WHERE RM_ID=@RM_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@RM_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@RM_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


