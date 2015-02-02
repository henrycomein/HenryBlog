using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Manage.Helper;
using Henry.Manage.BusinessLayer;
using Henry.Entity;

namespace Manage.Controllers
{
    public class BasicController : BaseController
    {
        //
        // GET: /BasicManage/

        public ActionResult Index()
        {
            return View();
        }
        #region 类别管理

        public ActionResult CategoryList()
        {
            var lstData = ArticleCategoryBLL.GetTreeList();
            return View(lstData);
        }

        public ActionResult Category(int id = 0)
        {
            ArticleCategory info = new ArticleCategory();
            if (id > 0) info = ArticleCategoryBLL.GetSingleOrDefault(id);
            return View(info);
        }

        public ActionResult CategorySelect(int id = 0)
        {
            ViewBag.ID = id;
            return View();
        }

        #endregion

        public ActionResult MenuList()
        {
            return View();
        }
        public ActionResult MenuSelect(int id = 0)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult TagSelect(int id = 0)
        {
            ViewBag.ID = id;
            var tags = TagBLL.GetList(new Tag { T_Status = 1 });
            return View(tags);
        }
        public ActionResult UserList()
        {
            return View();
        }


    }
}
