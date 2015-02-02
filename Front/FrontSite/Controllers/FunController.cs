using Henry.BusinessLayer;
using Henry.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontSite.Controllers
{
    public class FunController : Controller
    {
        //
        // GET: /Fun/

        public ActionResult Photo()
        {
            var data = PhotoCategoryBLL.GetList(new PhotoCategory { PC_Status = 1, PC_Show = 1 });
            return View(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">photo category id</param>
        /// <returns></returns>
        public ActionResult PhotoView(int id)
        {
            var categorydata = PhotoCategoryBLL.GetSingleOrDefault(id);
            if (categorydata != null)
            {
                ViewBag.CategoryID = id;
                ViewBag.CategoryName = categorydata.PC_Name;
                var hasValicate = true;
                //检查是否需要密码查看
                if (categorydata.PC_NeedPassword == 1)
                {
                    hasValicate = false;
                    var cookiename=Henry.Common.EncryptionHelper.EncryptionPassword(id.ToString(),categorydata.PC_Password);
                    var cookie = Request.Cookies[cookiename];
                    if (cookie != null) {
                        if (cookie.Value == "1") {
                            hasValicate = true;
                        }
                    }
                }
                var data = new List<Photo>();
                if (hasValicate)
                {
                    data = PhotoBLL.GetList(new Photo { P_Status = 1, P_CategoryID = id, OrderBy = "P_CreateTime desc" });
                    ViewBag.ValidRequest = 1;
                }
                else
                {
                    ViewBag.ValidRequest = 0;
                }
                return View(data);
            }
            else {
                return HttpNotFound("您所访问的页面不存在！");
            }
           
        }

        public ActionResult LifeEvent()
        {
            var hasValicate = false;
            //检查是否已经输入密码
            var cookiename = Henry.Common.EncryptionHelper.EncryptionPassword("LifeEvent", "1");
            var cookie = Request.Cookies[cookiename];
            if (cookie != null)
            {
                if (cookie.Value == "1")
                {
                    hasValicate = true;
                }
            }
            var data = new IndexEvent() ;
            if (hasValicate)
            {
                data = LifeEventBLL.GetIndexEvents();
                ViewBag.ValidRequest = 1;
            }
            else
            {
                ViewBag.ValidRequest = 0;
            }
            return View(data);
        }

        [HttpPost]
        public JsonResult CheckPhotoPassword(int id,string password)
        {
            var resultObj = new JObject();
            var success = false;
            if (id > 0)
            {
                var photocategory = PhotoCategoryBLL.GetSingleOrDefault(id);
                if (photocategory != null)
                {
                    if (photocategory.PC_Password.Equals(password)) { 
                        success = true;
                        resultObj["url"] = Url.Content("~/photoview/" + id.ToString());
                        //将成功信息写入即时cookie
                        var cookiename=Henry.Common.EncryptionHelper.EncryptionPassword(id.ToString(),password);
                        HttpCookie cookie =new HttpCookie(cookiename);
                        cookie.Value = "1";
                        Response.Cookies.Add(cookie);
                    }
                }
            }
            resultObj["success"] = success ? 1 : 0;
            return new NewtonJsonResult() { Data = resultObj };
        }

        [HttpPost]
        public JsonResult CheckEventPassword(string password)
        {
            var resultObj = new JObject();
            var success = false;
            var entity = SystemSettingBLL.GetSingleOrDefault("LifeEventPassword");
            if (entity.SS_Value.Equals(password))
            {
                success = true;
                resultObj["url"] = Url.Content("~/event.html");
                //将成功信息写入即时cookie
                var cookiename = Henry.Common.EncryptionHelper.EncryptionPassword("LifeEvent", "1");
                HttpCookie cookie = new HttpCookie(cookiename);
                cookie.Value = "1";
                Response.Cookies.Add(cookie);
            }
            resultObj["success"] = success ? 1 : 0;
            return new NewtonJsonResult() { Data = resultObj };
        }
    }
}
