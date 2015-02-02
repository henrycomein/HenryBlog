using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class TagDAL
    {
        public static DataTable GetList(Tag condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [Tag] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Tag condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "T_CreateTime DESC";
            var data = new 
            {
                TableName = "Tag",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,T_ID,T_Name,T_IsPhoto,T_Sort,T_Status,T_CreateTime,", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(Tag condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.T_ID <= 0)
            {
                cmdString = "INSERT INTO [Tag](T_Name,T_IsPhoto,T_Sort,T_Status) VALUES(@T_Name,@T_IsPhoto,@T_Sort,AT_Status)";
                paramer = new SqlParameter[4];
                paramer[1] = new SqlParameter("@T_Name",condition.T_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@T_IsPhoto",condition.T_IsPhoto);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@T_Sort",condition.T_Sort);
                paramer[3].SqlDbType = SqlDbType.Int; 
                paramer[4] = new SqlParameter("@T_Status",condition.T_Status);
                paramer[4].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString = "UPDATE [Tag] SET T_Name=@T_Name,T_IsPhoto=@T_IsPhoto,T_Sort=@T_Sort,T_Status=@T_Status WHERE T_ID=@T_ID";
                paramer = new SqlParameter[5];
                paramer[0] = new SqlParameter("@T_ID",condition.T_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@T_Name",condition.T_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;
                paramer[2] = new SqlParameter("@T_IsPhoto", condition.T_IsPhoto);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@T_Sort",condition.T_Sort);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@T_Status",condition.T_Status);
                paramer[4].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Tag] SET T_Status=@T_Status WHERE T_ID=@T_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@T_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@T_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


