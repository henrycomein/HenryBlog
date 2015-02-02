using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class LifeEventCategoryDAL
    {
        public static DataTable GetList(LifeEventCategory condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE LEC_Status<>2 ");
            var queryString = "SELECT * FROM [LifeEventCategory] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(LifeEventCategory condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "LEC_Status DESC";
            var data = new 
            {
                TableName = "LifeEventCategory",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,LEC_ID,LEC_Name,LEC_Sort,LEC_Status",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE LEC_Status<>2");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(LifeEventCategory condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.LEC_ID <= 0)
            {
                cmdString = "INSERT INTO [LifeEventCategory](LEC_Name,LEC_Sort) VALUES(@LEC_Name,@LEC_Sort)";
                paramer = new SqlParameter[2];
                paramer[0] = new SqlParameter("@LEC_Name",condition.LEC_Name);
                paramer[0].SqlDbType = SqlDbType.NVarChar; 
                paramer[0].Size = 20;  
                paramer[1] = new SqlParameter("@LEC_Sort",condition.LEC_Sort);
                paramer[1].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [LifeEventCategory] SET LEC_Name=@LEC_Name,LEC_Sort=@LEC_Sort WHERE LEC_ID=@LEC_ID";
                paramer = new SqlParameter[3];
                paramer[0] = new SqlParameter("@LEC_ID",condition.LEC_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@LEC_Name",condition.LEC_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 20;  
                paramer[2] = new SqlParameter("@LEC_Sort",condition.LEC_Sort);
                paramer[2].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [LifeEventCategory] SET LEC_Sort=@LEC_Sort WHERE LEC_ID=@LEC_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@LEC_ID",id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@LEC_Sort",status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


