using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class PhotoDAL
    {
        public static DataTable GetList(Photo condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE P_Status<>2 ");
            if (condition.P_ID > 0) { sqlCondition.AppendFormat(" AND P_ID={0}", condition.P_ID); }
            if (condition.P_CategoryID > 0) { sqlCondition.AppendFormat(" AND P_CategoryID={0}", condition.P_CategoryID); }
            var queryString = "SELECT p.*,T_Name AS P_TagName FROM dbo.Photo p INNER JOIN Tag on P_TagID=T_ID  " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Photo condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "P_CreateTime DESC";
            var data = new 
            {
                TableName = "Photo p INNER JOIN Tag on P_TagID=T_ID",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,P_ID,P_FileName,P_Desc,P_TagID,P_CategoryID,P_Status,P_CreateTime,T_Name AS P_TagName", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE P_Status<>2 ");
            if (condition.P_CategoryID > 0) { sqlCondition.AppendFormat(" AND P_CategoryID={0}", condition.P_CategoryID); }
            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(Photo condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.P_ID <= 0)
            {
                cmdString = "INSERT INTO [Photo](P_FileName,P_Desc,P_TagID,P_CategoryID,P_Status) VALUES(@P_FileName,@P_Desc,@P_TagID,@P_CategoryID,@P_Status)";
                paramer = new SqlParameter[5];
                paramer[0] = new SqlParameter("@P_FileName",condition.P_FileName);
                paramer[0].SqlDbType = SqlDbType.NVarChar; 
                paramer[0].Size = 50;  
                paramer[1] = new SqlParameter("@P_Desc",condition.P_Desc ?? string.Empty);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 100;  
                paramer[2] = new SqlParameter("@P_TagID",condition.P_TagID);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@P_CategoryID",condition.P_CategoryID);
                paramer[3].SqlDbType = SqlDbType.Int; 
                paramer[4] = new SqlParameter("@P_Status",condition.P_Status);
                paramer[4].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [Photo] SET P_FileName=@P_FileName,P_Desc=@P_Desc,P_TagID=@P_TagID,P_CategoryID=@P_CategoryID,P_Status=@P_Status WHERE P_ID=@P_ID";
                paramer = new SqlParameter[6];
                paramer[0] = new SqlParameter("@P_ID",condition.P_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@P_FileName",condition.P_FileName);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;
                paramer[2] = new SqlParameter("@P_Desc", condition.P_Desc ?? string.Empty);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 100;  
                paramer[3] = new SqlParameter("@P_TagID",condition.P_TagID);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@P_CategoryID",condition.P_CategoryID);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@P_Status",condition.P_Status);
                paramer[5].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [Photo] SET P_Status=@P_Status WHERE P_ID=@P_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@P_ID",id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@P_Status",status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
       
    }
}


