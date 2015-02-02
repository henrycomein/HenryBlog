using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class BackgroundImageDAL
    {
        public static DataTable GetList(BackgroundImage condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [BackgroundImage] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(BackgroundImage condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "BG_Status DESC";
            var data = new 
            {
                TableName = "BackgroundImage",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,BG_ID,BG_Name,BG_Status,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(BackgroundImage condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.BG_ID <= 0)
            {
                cmdString = "INSERT INTO [BackgroundImage](BG_Name) VALUES(@BG_Name)";
                paramer = new SqlParameter[1];
                paramer[1] = new SqlParameter("@BG_Name",condition.BG_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 50;  
            }
            else
            {
                cmdString ="UPDATE [BackgroundImage] SET BG_Name=@BG_Name WHERE BG_ID=@BG_ID";
                paramer = new SqlParameter[2];
                paramer[0] = new SqlParameter("@BG_ID",condition.BG_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@BG_Name",condition.BG_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;  
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [BackgroundImage] SET BG_Name=@BG_Name WHERE BG_ID=@BG_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@BG_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@BG_Name", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


