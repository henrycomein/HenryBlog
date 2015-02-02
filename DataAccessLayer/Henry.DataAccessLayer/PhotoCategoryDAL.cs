using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;

namespace Henry.DataAccessLayer
{
    public class PhotoCategoryDAL
    {
        public static DataTable GetList(PhotoCategory condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE PC_Status=1 ");
            if (condition.PC_ID>0) sqlCondition.AppendFormat(" AND PC_ID={0}", condition.PC_ID);
            var queryString = "SELECT * FROM PhotoCategory " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(PhotoCategory condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "PC_CreateTime DESC";
            var data = new 
            {
                TableName = "PhotoCategory",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,PC_ID,PC_Name,PC_CoverPhotoID,PC_Sort,PC_Desc,PC_Show,PC_Password,PC_NeedPassword,PC_Status,PC_CreateTime",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE PC_Status=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
    }
}


