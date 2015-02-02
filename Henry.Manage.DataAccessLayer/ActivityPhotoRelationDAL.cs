using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class ActivityPhotoRelationDAL
    {
        public static DataTable GetList(ActivityPhotoRelation condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM [ActivityPhotoRelation] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(ActivityPhotoRelation condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "APR_CreateTime DESC";
            var data = new 
            {
                TableName = "ActivityPhotoRelation",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,APR_ID,APR_ActivityID,APR_PhotoID,APR_Sort,APR_Status,APR_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(ActivityPhotoRelation condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.APR_ID <= 0)
            {
                cmdString = "INSERT INTO [ActivityPhotoRelation](APR_ActivityID,APR_PhotoID,APR_Sort,APR_Status) VALUES(@APR_ActivityID,@APR_PhotoID,@APR_Sort,@APR_Status)";
                paramer = new SqlParameter[4];
                paramer[1] = new SqlParameter("@APR_ActivityID",condition.APR_ActivityID);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@APR_PhotoID",condition.APR_PhotoID);
                paramer[2].SqlDbType = SqlDbType.Int; 
                paramer[3] = new SqlParameter("@APR_Sort",condition.APR_Sort);
                paramer[3].SqlDbType = SqlDbType.Int; 
                paramer[4] = new SqlParameter("@APR_Status",condition.APR_Status);
                paramer[4].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [ActivityPhotoRelation] SET APR_ActivityID=@APR_ActivityID,APR_PhotoID=@APR_PhotoID,APR_Sort=@APR_Sort,APR_Status=@APR_Status WHERE APR_ID=@APR_ID";
                paramer = new SqlParameter[5];
                paramer[0] = new SqlParameter("@APR_ID",condition.APR_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@APR_ActivityID",condition.APR_ActivityID);
                paramer[1].SqlDbType = SqlDbType.Int;
                paramer[2] = new SqlParameter("@APR_PhotoID",condition.APR_PhotoID);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@APR_Sort",condition.APR_Sort);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@APR_Status",condition.APR_Status);
                paramer[4].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [ActivityPhotoRelation] SET APR_Status=@APR_Status WHERE APR_ID=@APR_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@APR_ID",id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@APR_Status",status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


