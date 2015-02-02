using Henry.Manage.BusinessLayer;
using Manage.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manage.Controllers.API
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [HttpPost]
        public NewtonJsonResult Login(string username,string password)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = UserBLL.CheckPassword(username, password);
            resultObj.success = result ? 1 : 0;
            resultObj.url = "/";
            resultObj.message =  result ? "验证成功！" : "用户名或密码错误！" ;
            if(result)System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
            return new NewtonJsonResult() { Data = resultObj };
        }

    }
}
