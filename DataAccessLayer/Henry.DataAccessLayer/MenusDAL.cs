using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;
namespace Henry.DataAccessLayer
{
    public class MenusDAL
    {
        public static DataTable GetList(Menus condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            var queryString = "SELECT * FROM Menus " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(Menus condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "M_CreateTime DESC";
            var data = new 
            {
                TableName = "Menus",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,M_ID,M_Name,M_Url,M_ParentID,M_OrderIndex,M_Status,M_CreateTime,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
    }
}


