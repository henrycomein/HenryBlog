using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class MenusDAL
    {

        public static DataTable GetTree(int id)
        {
            return MySqlHelper.ExecuteQueryList(string.Format("EXEC GetMenuTree {0}",id));
        }
        public static DataTable GetList(Menus condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE M_Status<>2 ");
            var queryString = "SELECT *,dbo.GetFullMenuName(M_ParentID) AS M_ParentFullName FROM [Backend_Menus] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Menus condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "M_CreateTime DESC";
            var data = new 
            {
                TableName = "Backend_Menus",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,M_ID,M_Name,M_Url,M_ParentID,M_OrderIndex,M_Status,M_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(Menus condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.M_ID <= 0)
            {
                cmdString = "INSERT INTO [Backend_Menus](M_Name,M_Url,M_ParentID,M_OrderIndex,M_Status) VALUES(@M_Name,@M_Url,@M_ParentID,@M_OrderIndex,@M_Status)";
                paramer = new SqlParameter[5];
                paramer[0] = new SqlParameter("@M_Name",condition.M_Name);
                paramer[0].SqlDbType = SqlDbType.NVarChar; 
                paramer[0].Size = 50;  
                paramer[1] = new SqlParameter("@M_Url",condition.M_Url);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 100;  
                paramer[2] = new SqlParameter("@M_ParentID",condition.M_ParentID);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@M_OrderIndex",condition.M_OrderIndex);
                paramer[3].SqlDbType = SqlDbType.Int; 
                paramer[4] = new SqlParameter("@M_Status",condition.M_Status);
                paramer[4].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [Backend_Menus] SET M_Name=@M_Name,M_Url=@M_Url,M_ParentID=@M_ParentID,M_OrderIndex=@M_OrderIndex,M_Status=@M_Status WHERE M_ID=@M_ID";
                paramer = new SqlParameter[6];
                paramer[0] = new SqlParameter("@M_ID",condition.M_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@M_Name",condition.M_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@M_Url",condition.M_Url);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 100;  
                paramer[3] = new SqlParameter("@M_ParentID",condition.M_ParentID);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@M_OrderIndex",condition.M_OrderIndex);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@M_Status",condition.M_Status);
                paramer[5].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Backend_Menus] SET M_Status=@M_Status WHERE M_ID=@M_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@M_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@M_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


