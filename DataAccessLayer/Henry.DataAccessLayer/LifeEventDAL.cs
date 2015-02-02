using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;

namespace Henry.DataAccessLayer
{
    public class LifeEventDAL
    {
        public static DataTable GetList(LifeEvent condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE LE_Status=1 ");
            var queryString = "SELECT * FROM LifeEvent " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(LifeEvent condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "LE_CreateTime DESC";
            var data = new 
            {
                TableName = "LifeEvent",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,LE_ID,LE_Date,LE_CateogryID,LE_Title,LE_Desc,LE_Status,LE_CreateTime",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE LE_Status=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        public static DataSet GetIndexEvents()
        {
            return MySqlHelper.ExecuteCustomQuery("exec dbo.GetLifeEvent");
        }
    }
}


