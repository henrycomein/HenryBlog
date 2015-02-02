using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class SystemSettingDAL
    {
        public static DataTable GetList(SystemSetting condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [SystemSetting] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(SystemSetting condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "SS_Value DESC";
            var data = new 
            {
                TableName = "SystemSetting",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,SS_ID,SS_Code,SS_Name,SS_Value,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(SystemSetting condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.SS_ID <= 0)
            {
                cmdString = "INSERT INTO [SystemSetting](SS_Code,SS_Name) VALUES(@SS_Code,@SS_Name)";
                paramer = new SqlParameter[2];
                paramer[1] = new SqlParameter("@SS_Code",condition.SS_Code);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@SS_Name",condition.SS_Name);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = 20;  
            }
            else
            {
                cmdString ="UPDATE [SystemSetting] SET SS_Code=@SS_Code,SS_Name=@SS_Name WHERE SS_ID=@SS_ID";
                paramer = new SqlParameter[3];
                paramer[0] = new SqlParameter("@SS_ID",condition.SS_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@SS_Code",condition.SS_Code);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@SS_Name",condition.SS_Name);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 20;  
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [SystemSetting] SET SS_Name=@SS_Name WHERE SS_ID=@SS_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@SS_ID",id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@SS_Name",status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


