using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Manage.Helper;
using Henry.Manage.BusinessLayer;
using Henry.Entity;
using Henry.Common;
namespace Manage.Controllers
{
    public class BusinessController : BaseController
    {
        //
        // GET: /Business/

        public ActionResult Index()
        {
            return View();
        }
         
        #region 文章管理

        public ActionResult ArticleList()
        {
            return View();
        }
       
        public ActionResult Article(int id=0)
        {
            Article info=new Article();
            if (id > 0) info = ArticleBLL.GetSingleOrDefault(id);
            return View(info);
        }

        #endregion

        #region 相册管理
        public ActionResult PhotoCategoryList()
        {
            List<PhotoCategory> data = PhotoCategoryBLL.GetList(new PhotoCategory());
            return View(data);
        }
        public ActionResult PhotoCategory(int id=0)
        {
            var pc = new PhotoCategory() { };
            if (id > 0)
            { 
                pc= PhotoCategoryBLL.GetSingleOrDefault(id);
            }
            return View(pc);
        }

        public ActionResult PhotoList(int id=0)
        {
            List<Photo> data = PhotoBLL.GetList(new Photo() { P_CategoryID = id });
            ViewBag.CategoryName = PhotoCategoryBLL.GetSingleOrDefault(id).PC_Name;
            ViewBag.CategoryID = id;
            ViewBag.BasicPath = SettingHelper.PhotoVisitePath();
            return View(data);
        }

        public ActionResult Photo(int id=0)
        {
            var p=new Photo(){};
            if(id>0)
            {
                p=PhotoBLL.GetSingleOrDefault(id);
            }
            var dropdownData=PhotoCategoryBLL.GetList(new PhotoCategory { PC_Status=1});
            ViewData["photo-category"] = new SelectList(dropdownData, "PC_ID", "PC_Name", p.P_CategoryID);
            return View(p);
        }
        #endregion

        #region 生活事件管理
        public ActionResult EventList()
        {
            return View();
        }
        public ActionResult Event(int id=0)
        { 
            var e =new LifeEvent();
            if (id > 0) {
                e = LifeEventBLL.GetSingleOrDefault(id);
            }
            ViewBag.Category = LifeEventCategoryBLL.GetList(new LifeEventCategory { LEC_Status = 1 });
            return View(e);
        }
        #endregion

        public ActionResult About()
        {
            var info = AboutMeBLL.Get();
            return View(info);
        }
    }
}
