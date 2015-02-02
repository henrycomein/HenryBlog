using Henry.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.BusinessLayer
{
    public class LifeEventBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<LifeEvent> GetList(LifeEvent condition)
        {
            return LifeEventDAL.GetList(condition).ToEntity<LifeEvent>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<LifeEvent> GetListWithPage(LifeEvent condition)
        {
            int totalcount=0;
            var result= LifeEventDAL.GetListWithPage(condition,out totalcount).ToEntity<LifeEvent>();;
            return new PageParamer<LifeEvent> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static LifeEvent GetSingleOrDefault(int keyval)
        {
            var condition = new LifeEvent  { LE_ID = keyval };
            return  LifeEventDAL.GetList(condition).ToEntity<LifeEvent>().FirstOrDefault();
        }

        public static IndexEvent GetIndexEvents()
        {
            var result = new IndexEvent();
            var data = LifeEventDAL.GetIndexEvents();
            result.CategoryData = data.Tables[0].ToEntity<LifeEventCategory>();
            result.YearData = data.Tables[1].ToEntity<YearData>();
            result.MonthData = data.Tables[2].ToEntity<MonthData>();
            result.MonthEvent = data.Tables[3].ToEntity<LifeEvent>();
            return result;
        }
        #endregion

        
    }
}

