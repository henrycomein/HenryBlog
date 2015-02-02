using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.BusinessLayer
{
    public class SystemSettingBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<SystemSetting> GetList(SystemSetting condition)
        {
            return SystemSettingDAL.GetList(condition).ToEntity<SystemSetting>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<SystemSetting> GetListWithPage(SystemSetting condition)
        {
            int totalcount=0;
            var result= SystemSettingDAL.GetListWithPage(condition,out totalcount).ToEntity<SystemSetting>();;
            return new PageParamer<SystemSetting> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static SystemSetting GetSingleOrDefault(string code)
        {
            var condition = new SystemSetting  { SS_Code = code };
            return  SystemSettingDAL.GetList(condition).ToEntity<SystemSetting>().FirstOrDefault();
        }

        #endregion

        
    }
}

