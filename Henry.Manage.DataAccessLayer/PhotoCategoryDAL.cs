using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class PhotoCategoryDAL
    {
        public static DataTable GetList(PhotoCategory condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE PC_Status<>2 ");
            if(condition.PC_ID>0) sqlCondition.AppendFormat(" AND PC_ID = {0}",condition.PC_ID);
            var queryString = @"SELECT pc.*,isnull(P_FileName,'') as PC_CoverPhotoFileName FROM PhotoCategory  pc LEFT JOIN Photo ON PC_CoverPhotoID=P_ID " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(PhotoCategory condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "PC_CreateTime DESC";
            var data = new 
            {
                TableName = "PhotoCategory",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,PC_ID,PC_Name,PC_CoverPhotoID,PC_Password,PC_NeedPassword,PC_Show,PC_Sort,PC_Desc,PC_Status,PC_CreateTime", condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE PC_Status<>2");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
        public static bool AddOrUpdate(PhotoCategory condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.PC_ID <= 0)
            {
                cmdString = "INSERT INTO [PhotoCategory](PC_Name,PC_Sort,PC_Desc,PC_Password,PC_NeedPassword,PC_Show,PC_Status) VALUES(@PC_Name,@PC_Sort,@PC_Desc,@PC_Password,@PC_NeedPassword,@PC_Show,@PC_Status)";
                paramer = new SqlParameter[7];
                paramer[0] = new SqlParameter("@PC_Name",condition.PC_Name);
                paramer[0].SqlDbType = SqlDbType.NVarChar;
                paramer[0].Size = 50; 
                paramer[1] = new SqlParameter("@PC_Sort",condition.PC_Sort);
                paramer[1].SqlDbType = SqlDbType.Int; 
                paramer[2] = new SqlParameter("@PC_Desc",condition.PC_Desc);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = 100;  
                paramer[3] = new SqlParameter("@PC_Status",condition.PC_Status);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@PC_NeedPassword", condition.PC_NeedPassword);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@PC_Password", condition.PC_Password);
                paramer[5].SqlDbType = SqlDbType.NVarChar;
                paramer[5].Size =20;
                paramer[6] = new SqlParameter("@PC_Show", condition.PC_Show);
                paramer[6].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString = "UPDATE [PhotoCategory] SET PC_Name=@PC_Name,PC_Sort=@PC_Sort,PC_Desc=@PC_Desc,PC_Password=@PC_Password,PC_NeedPassword=@PC_NeedPassword,PC_Show=@PC_Show,PC_Status=@PC_Status WHERE PC_ID=@PC_ID";
                paramer = new SqlParameter[8];
                paramer[0] = new SqlParameter("@PC_ID",condition.PC_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@PC_Name",condition.PC_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50; 
                paramer[2] = new SqlParameter("@PC_Sort",condition.PC_Sort);
                paramer[2].SqlDbType = SqlDbType.Int;
                paramer[3] = new SqlParameter("@PC_Desc",condition.PC_Desc);
                paramer[3].SqlDbType = SqlDbType.NVarChar;
                paramer[3].Size = 100;  
                paramer[4] = new SqlParameter("@PC_Status",condition.PC_Status);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@PC_NeedPassword", condition.PC_NeedPassword);
                paramer[5].SqlDbType = SqlDbType.Int;
                paramer[6] = new SqlParameter("@PC_Password", condition.PC_Password);
                paramer[6].SqlDbType = SqlDbType.NVarChar;
                paramer[6].Size = 20;
                paramer[7] = new SqlParameter("@PC_Show", condition.PC_Show);
                paramer[7].SqlDbType = SqlDbType.Int; 
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [PhotoCategory] SET PC_Status=@PC_Status WHERE PC_ID=@PC_ID";
            var paramer=new SqlParameter[2];
            paramer[0] = new SqlParameter("@PC_ID",id);
            paramer[0].SqlDbType= SqlDbType.Int;

            paramer[1] = new SqlParameter("@PC_Status",status);
            paramer[1].SqlDbType= SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="photoid">照片id</param>
        /// <param name="categoryid">类别id</param>
        /// <returns></returns>
        public static bool SetCover(int photoid,int categoryid)
        {
            var cmdString = "UPDATE [PhotoCategory] SET PC_CoverPhotoID=@PC_CoverPhotoID WHERE PC_ID=@PC_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@PC_ID", categoryid);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@PC_CoverPhotoID", photoid);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


