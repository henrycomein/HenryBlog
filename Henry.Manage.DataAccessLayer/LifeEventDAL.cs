using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class LifeEventDAL
    {
        public static DataTable GetList(LifeEvent condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE LE_Status<>2 ");
            if (condition.LE_ID > 0) sqlCondition.AppendFormat(" AND LE_ID={0}", condition.LE_ID);
            var queryString = "SELECT * FROM [LifeEvent] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(LifeEvent condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "LE_CreateTime DESC";
            var data = new 
            {
                TableName = "LifeEvent",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,LE_ID,LE_Date,LE_Title,LE_Desc,LE_Status", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE LE_Status<>2");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(LifeEvent condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.LE_ID <= 0)
            {
                cmdString = "INSERT INTO [LifeEvent](LE_Date,LE_CateogryID,LE_Title,LE_Images,LE_Desc,LE_Status) VALUES(@LE_Date,@LE_CateogryID,@LE_Title,@LE_Images,@LE_Desc,@LE_Status)";
                paramer = new SqlParameter[6];
                paramer[0] = new SqlParameter("@LE_Title", condition.LE_Title);
                paramer[0].SqlDbType = SqlDbType.NVarChar; 
                paramer[0].Size = 50;
                paramer[1] = new SqlParameter("@LE_Desc", condition.LE_Desc);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 500;
                paramer[2] = new SqlParameter("@LE_Status", condition.LE_Status);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@LE_Date", condition.LE_Date);
                paramer[3].SqlDbType = SqlDbType.DateTime;
                paramer[4] = new SqlParameter("@LE_CateogryID", condition.LE_CateogryID);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@LE_Images", condition.LE_Images);
                paramer[5].SqlDbType = SqlDbType.NVarChar;
                paramer[5].Size = 200;
            }
            else
            {
                cmdString = "UPDATE [LifeEvent] SET LE_Title=@LE_Title,LE_Images=@LE_Images,LE_CateogryID=@LE_CateogryID,LE_Date=@LE_Date,LE_Desc=@LE_Desc,LE_Status=@LE_Status WHERE LE_ID=@LE_ID";
                paramer = new SqlParameter[7];
                paramer[0] = new SqlParameter("@LE_ID", condition.LE_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@LE_Title", condition.LE_Title);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;
                paramer[2] = new SqlParameter("@LE_Desc", condition.LE_Desc);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 500;
                paramer[3] = new SqlParameter("@LE_Status", condition.LE_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@LE_Date", condition.LE_Date);
                paramer[4].SqlDbType = SqlDbType.DateTime;
                paramer[5] = new SqlParameter("@LE_Images", condition.LE_Images);
                paramer[5].SqlDbType = SqlDbType.NVarChar;
                paramer[5].Size = 200;
                if (string.IsNullOrEmpty(condition.LE_Images)) paramer[5].Value = DBNull.Value;
                paramer[6] = new SqlParameter("@LE_CateogryID", condition.LE_CateogryID);
                paramer[6].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [LifeEvent] SET LE_Status=@LE_Status WHERE LE_ID=@LE_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@LE_ID", id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@LE_Status", status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


