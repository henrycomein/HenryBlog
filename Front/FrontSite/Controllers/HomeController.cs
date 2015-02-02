using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Henry.BusinessLayer;
using Henry.Entity;
namespace FrontSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var pics = new StringBuilder();
            for (int i = 1; i <= 20; i++)
            {
                pics.AppendFormat("{0}.jpg,", i);
            }
            ViewBag.BGImageList = pics.ToString().TrimEnd(new char[] { ',' });
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "关于我";
            var info = AboutMeBLL.Get();
            return View(info);
        }

        public ActionResult OpenSource()
        {
            return View();
        }

        
        public ActionResult _NavCategory()
        {
            return PartialView(ArticleCategoryBLL.GetTopCategories());
        }

    }
}
