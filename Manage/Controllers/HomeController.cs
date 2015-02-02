using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Manage.Helper;
using Henry.Manage.BusinessLayer;

namespace Manage.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "IndexPage";
            SetCommonLoginUserInfo();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public NewtonJsonResult Login(string account,string password)
        {
            var resultObj = new AjaxHandleDataResult();
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                resultObj.message = "请输入账号或密码！";
                resultObj.success = 0;
            }
            else {
                if (UserBLL.CheckPassword(account, password))
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(account, false);
                    resultObj.success = 1;
                    resultObj.url = Url.Action("Index");
                }
                else
                {
                    resultObj.message = "请输入账号或密码！";
                    resultObj.success = 0;
                }
            }   
            var result=new NewtonJsonResult();
            result.Data = resultObj;
            return result;
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            return Redirect(Url.Content("~/login"));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult FontIcons()
        {
            return View();
        }
    }
}
