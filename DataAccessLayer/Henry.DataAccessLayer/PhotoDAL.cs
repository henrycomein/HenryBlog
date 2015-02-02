using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;

namespace Henry.DataAccessLayer
{
    public class PhotoDAL
    {
        public static DataTable GetList(Photo condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE P_Status=1 ");
            if (condition.P_CategoryID > 0) sqlCondition.AppendFormat(" AND P_CategoryID={0}", condition.P_CategoryID);
            var queryString = "SELECT * FROM Photo " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Photo condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "P_CreateTime DESC";
            var data = new 
            {
                TableName = "Photo",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,P_ID,P_FileName,P_Desc,P_TagID,P_CategoryID,P_Status,P_CreateTime",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE P_Status=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
    }
}


