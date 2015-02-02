using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
using System.Data.SqlClient;

namespace Henry.Manage.DataAccessLayer
{
    public class ArticleCategoryDAL
    {
        public static DataTable GetTree(int id)
        {
            return MySqlHelper.ExecuteQueryList(string.Format("exec [GetCategoryTree] {0}",id));
        }
        public static DataTable GetTreeList()
        { 
            return MySqlHelper.ExecuteQueryList("exec [GetCategoryTreeList]");
        }
        public static DataTable GetList(ArticleCategory condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE AC_Status<>2 ");
            if (condition.AC_ID > 0) sqlCondition.AppendFormat(" AND AC_ID={0}", condition.AC_ID);
            if (condition.AC_Status > -1) sqlCondition.AppendFormat(" AND AC_Status={0}", condition.AC_Status);
            var queryString = "SELECT *,dbo.GetFullCategoryName(AC_ParentID) as AC_ParentFullName FROM [ArticleCategory] " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(ArticleCategory condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "AC_Sort ASC,AC_CreateTime DESC";
            var data = new 
            {
                TableName = "ArticleCategory",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,AC_ID,AC_Name,AC_Code,AC_ParentID,AC_ShowFront,AC_Description,AC_Sort,AC_PicName,AC_ShowList,AC_IsComplete,AC_Status,AC_CreateTime",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }

        public static bool AddOrUpdate(ArticleCategory condition)
        {
            var cmdString = string.Empty;
            SqlParameter[] paramer;
            if (condition.AC_ID <= 0)
            {
                cmdString = "INSERT INTO [ArticleCategory](AC_Name,AC_Code,AC_ParentID,AC_ShowFront,AC_Description,AC_Sort,AC_PicName,AC_ShowList,AC_IsComplete,AC_Status) VALUES(@AC_Name,@AC_Code,@AC_ParentID,@AC_ShowFront,@AC_Description,@AC_Sort,@AC_PicName,@AC_ShowList,@AC_IsComplete,@AC_Status)";
                paramer = new SqlParameter[10];
                paramer[1] = new SqlParameter("@AC_Name",condition.AC_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar; 
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@AC_Code",condition.AC_Code);
                paramer[2].SqlDbType = SqlDbType.NVarChar; 
                paramer[2].Size = 50;  
                paramer[3] = new SqlParameter("@AC_ParentID",condition.AC_ParentID);
                paramer[3].SqlDbType = SqlDbType.Int; 
                paramer[4] = new SqlParameter("@AC_ShowFront",condition.AC_ShowFront);
                paramer[4].SqlDbType = SqlDbType.Int; 
                paramer[5] = new SqlParameter("@AC_Description",condition.AC_Description);
                paramer[5].SqlDbType = SqlDbType.NVarChar; 
                paramer[5].Size = 2000;  
                paramer[6] = new SqlParameter("@AC_Sort",condition.AC_Sort);
                paramer[6].SqlDbType = SqlDbType.Int;
                paramer[7] = new SqlParameter("@AC_PicName", condition.AC_PicName);
                paramer[7].SqlDbType = SqlDbType.NVarChar; 
                paramer[7].Size = 100;
                paramer[7].IsNullable = true;
                if (condition.AC_PicName == null)
                {
                    paramer[7].Value = DBNull.Value;
                }
                paramer[8] = new SqlParameter("@AC_ShowList",condition.AC_ShowList);
                paramer[8].SqlDbType = SqlDbType.Int; 
                paramer[9] = new SqlParameter("@AC_IsComplete",condition.AC_IsComplete);
                paramer[9].SqlDbType = SqlDbType.Int; 
                paramer[10] = new SqlParameter("@AC_Status",condition.AC_Status);
                paramer[10].SqlDbType = SqlDbType.Int; 
            }
            else
            {
                cmdString ="UPDATE [ArticleCategory] SET AC_Name=@AC_Name,AC_Code=@AC_Code,AC_ParentID=@AC_ParentID,AC_ShowFront=@AC_ShowFront,AC_Description=@AC_Description,AC_Sort=@AC_Sort,AC_PicName=@AC_PicName,AC_ShowList=@AC_ShowList,AC_IsComplete=@AC_IsComplete,AC_Status=@AC_Status WHERE AC_ID=@AC_ID";
                paramer = new SqlParameter[11];
                paramer[0] = new SqlParameter("@AC_ID",condition.AC_ID);
                paramer[0].SqlDbType = SqlDbType.Int;
                paramer[1] = new SqlParameter("@AC_Name",condition.AC_Name);
                paramer[1].SqlDbType = SqlDbType.NVarChar;
                paramer[1].Size = 50;  
                paramer[2] = new SqlParameter("@AC_Code",condition.AC_Code);
                paramer[2].SqlDbType = SqlDbType.NVarChar;
                paramer[2].Size = 50;  
                paramer[3] = new SqlParameter("@AC_ParentID",condition.AC_ParentID);
                paramer[3].SqlDbType = SqlDbType.Int;
                paramer[4] = new SqlParameter("@AC_ShowFront",condition.AC_ShowFront);
                paramer[4].SqlDbType = SqlDbType.Int;
                paramer[5] = new SqlParameter("@AC_Description",condition.AC_Description);
                paramer[5].SqlDbType = SqlDbType.NVarChar;
                paramer[5].Size = 2000;  
                paramer[6] = new SqlParameter("@AC_Sort",condition.AC_Sort);
                paramer[6].SqlDbType = SqlDbType.Int;
                paramer[7] = new SqlParameter("@AC_PicName",condition.AC_PicName);
                paramer[7].SqlDbType = SqlDbType.NVarChar;
                paramer[7].Size = 100;
                paramer[7].IsNullable = true;
                if (condition.AC_PicName == null)
                {
                    paramer[7].Value = DBNull.Value;
                }
                paramer[8] = new SqlParameter("@AC_ShowList",condition.AC_ShowList);
                paramer[8].SqlDbType = SqlDbType.Int;
                paramer[9] = new SqlParameter("@AC_IsComplete",condition.AC_IsComplete);
                paramer[9].SqlDbType = SqlDbType.Int;
                paramer[10] = new SqlParameter("@AC_Status",condition.AC_Status);
                paramer[10].SqlDbType = SqlDbType.Int;
            }
            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
        public static bool UpdateStatus(int id, int status)
        {
            var cmdString = "UPDATE [ArticleCategory] SET AC_Status=@AC_Status WHERE AC_ID=@AC_ID";
            var paramer = new SqlParameter[2];
            paramer[0] = new SqlParameter("@AC_ID", id);
            paramer[0].SqlDbType = SqlDbType.Int;

            paramer[1] = new SqlParameter("@AC_Status", status);
            paramer[1].SqlDbType = SqlDbType.Int;

            return MySqlHelper.ExecuteNoQuery(cmdString, paramer);
        }
    }
}


