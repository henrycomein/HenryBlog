using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manage.Controllers
{
    [Authorize]
    public class BaseController:Controller
    {
        private Henry.Entity.UserDetail _currentUser;
        /// <summary>
        /// 当前登录人
        /// </summary>
        public Henry.Entity.UserDetail CurrentUser {
            get
            {
                if (_currentUser==null && Request.IsAuthenticated)
                {
                    _currentUser=Henry.Manage.BusinessLayer.UserBLL.GetSingleOrDefault(User.Identity.Name);
                }
                return _currentUser;
            }
            set { _currentUser = value; }
        }
        protected void SetCommonLoginUserInfo()
        {
            if (CurrentUser != null)
            {
                ViewBag.CurrentUser = CurrentUser;
            }
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (Request.IsAuthenticated)
            {
                if (Session["Menus"] != null)
                {
                    ViewBag.UserMenus = (List<Henry.Entity.Menus>)Session["Menus"];
                }
                else
                {
                    var menus = Henry.Manage.BusinessLayer.UserBLL.GetMenus(CurrentUser.UD_UserID);
                    Session["Menus"] = menus;
                    ViewBag.UserMenus = menus;
                }
            }
            return base.BeginExecuteCore(callback, state);
        }
    }
}