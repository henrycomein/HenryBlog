using Henry.Manage.DataAccessLayer;
using Henry.Entity;
using Henry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Manage.BusinessLayer
{
    public class UserBLL
    {
        #region GetData

        /// <summary>
        /// get all item with specify conditins
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        public static List<User> GetList(User condition)
        {
            return UserDAL.GetList(condition).ToEntity<User>();
        }

        /// <summary>
        /// get items with specify conditins,and return specified numbers of items
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>a list width items</returns>
        /// <remarks>condition.PageSize and condition.PageIndex must be defined,then get totalitems though condition.ItemTotalCount</remarks>
        public static PageParamer<User> GetListWithPage(User condition)
        {
            int totalcount=0;
            if (condition.PageSize < 0) condition.PageSize = 10;
            if (condition.PageIndex < 1) condition.PageIndex = 1;
            var result= UserDAL.GetListWithPage(condition,out totalcount).ToEntity<User>();;
            return new PageParamer<User> { Items = result,PageIndex=condition.PageIndex,PageSize=condition.PageSize, TotalCount = totalcount };
        }

        public static List<Menus> GetMenus(int userid)
        {
            return UserDAL.GetMenus(userid.ToString()).ToEntity<Menus>();
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static UserDetail GetSingleOrDefault(int keyval)
        {
            var condition = new User  { U_ID = keyval };
            return UserDAL.GetList(condition).ToEntity<UserDetail>().FirstOrDefault();
        }
        /// <summary>
        /// get single data
        /// </summary>
        /// <param name="keyval">primary key value</param>
        /// <returns>item</returns>
        /// <remarks>if found more than one data in database,then throws errors.</remarks>
        public static UserDetail GetSingleOrDefault(string account)
        {
            var condition = new User { U_Account = account,U_Status=1 };
            return UserDAL.GetList(condition).ToEntity<UserDetail>().FirstOrDefault();
        }
        public static Boolean CheckPassword(string account, string password)
        {
            account=account.ToLower();
            var check = false;
            var userinfo = GetSingleOrDefault(account);
            if (userinfo != null)
            {
                var pwd = Common.EncryptionHelper.EncryptionPassword(account, password);
                if (pwd == userinfo.U_Password) check = true;
            }
            return check;
        }
        #endregion

        #region handle data

        /// <summary>
        /// add data
        /// </summary>
        /// <param name="info">data infomation</param>
        /// <returns>success or fail</returns>
        public static bool Add(User info)
        {
            return UserDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// update data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool Update(User info)
        {
            return UserDAL.AddOrUpdate(info);
        }

        /// <summary>
        /// Disable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Disable(int id)
        {
            return UserDAL.UpdateStatus(id, 0); 
        }

        /// <summary>
        /// Enable data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Enable(int id)
        {
            return UserDAL.UpdateStatus(id, 1);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="id">data keyid</param>
        /// <returns></returns>
        public static bool Delete(int id)
        {
            return UserDAL.UpdateStatus(id, 2);
        }
        
        #endregion
    }
}

