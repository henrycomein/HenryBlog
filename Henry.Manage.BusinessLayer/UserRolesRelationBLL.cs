using Henry.Manage.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Manage.BusinessLayer
{
    public class UserRolesRelationBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<UserRolesRelation> GetList(UserRolesRelation condition)
        {
            return UserRolesRelationDAL.GetList(condition).ToEntity<UserRolesRelation>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<UserRolesRelation> GetListWithPage(UserRolesRelation condition)
        {
            int totalcount=0;
            if (condition.PageSize < 0) condition.PageSize = 10;
            if (condition.PageIndex < 1) condition.PageIndex = 1;
            var result= UserRolesRelationDAL.GetListWithPage(condition,out totalcount).ToEntity<UserRolesRelation>();;
            return new PageParamer<UserRolesRelation> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }
        
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static UserRolesRelation GetSingleOrDefault(int keyval)
        {
            var condition = new UserRolesRelation  { UR_ID = keyval };
            return  UserRolesRelationDAL.GetList(condition).ToEntity<UserRolesRelation>().FirstOrDefault();
        }

        #endregion

        #region handle data

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="info">data infomation</param>
        /// <returns>success or fail</returns>
        public static bool Add(UserRolesRelation info)
        {
            return UserRolesRelationDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(UserRolesRelation info)
        {
            return UserRolesRelationDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// Disable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Disable(int id)
        {
            return UserRolesRelationDAL.UpdateStatus(id, 0); 
        }

        /// <summary>
        /// Enable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Enable(int id)
        {
            return UserRolesRelationDAL.UpdateStatus(id, 1);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return UserRolesRelationDAL.UpdateStatus(id, 2);
        }
        
        #endregion
    }
}

