using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Henry.Entity;

namespace Henry.DataAccessLayer
{
    public class SystemSettingDAL
    {
        public static DataTable GetList(SystemSetting condition)
        {
            var sqlCondition=new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1 ");
            if (!string.IsNullOrEmpty(condition.SS_Code)) sqlCondition.AppendFormat(" AND SS_Code=N'{0}'", condition.SS_Code.CheckSqlParamer());
            var queryString = "SELECT * FROM SystemSetting " + sqlCondition;
            return MySqlHelper.ExecuteQueryList(queryString);
        }
        public static DataTable GetListWithPage(SystemSetting condition,out int totalcount)
        {
            if (string.IsNullOrEmpty(condition.OrderBy)) condition.OrderBy = "SS_Value DESC";
            var data = new 
            {
                TableName = "SystemSetting",
                ColName = string.Format("ROW_NUMBER() OVER(order by {0}) as ord,SS_ID,SS_Code,SS_Name,SS_Value,",condition.OrderBy),
                PageIndex=condition.PageIndex,
                PageSize=condition.PageSize
            };
            var sqlCondition = new StringBuilder(100);
            sqlCondition.Append("WHERE 1=1");

            return MySqlHelper.ExecuteQueryListWithPage(data.TableName, data.ColName, sqlCondition.ToString(), data.PageIndex, data.PageSize, out totalcount);
        }
        
    }
}


