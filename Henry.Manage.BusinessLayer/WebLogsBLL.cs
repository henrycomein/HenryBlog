using Henry.Manage.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Manage.BusinessLayer
{
    public class WebLogsBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<WebLogs> GetList(WebLogs condition)
        {
            return WebLogsDAL.GetList(condition).ToEntity<WebLogs>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<WebLogs> GetListWithPage(WebLogs condition)
        {
            int totalcount=0;
            if (condition.PageSize < 0) condition.PageSize = 10;
            if (condition.PageIndex < 1) condition.PageIndex = 1;
            var result= WebLogsDAL.GetListWithPage(condition,out totalcount).ToEntity<WebLogs>();;
            return new PageParamer<WebLogs> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static WebLogs GetSingleOrDefault(int keyval)
        {
            var condition = new WebLogs  { id = keyval };
            return  WebLogsDAL.GetList(condition).ToEntity<WebLogs>().FirstOrDefault();
        }

        #endregion

        #region handle data

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="info">data infomation</param>
        /// <returns>success or fail</returns>
        public static bool Add(WebLogs info)
        {
            return WebLogsDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(WebLogs info)
        {
            return WebLogsDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// Disable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Disable(int id)
        {
            return WebLogsDAL.UpdateStatus(id, 0); 
        }

        /// <summary>
        /// Enable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Enable(int id)
        {
            return WebLogsDAL.UpdateStatus(id, 1);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return WebLogsDAL.UpdateStatus(id, 2);
        }
        
        #endregion
    }
}

