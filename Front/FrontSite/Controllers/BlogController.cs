using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Henry.BusinessLayer;
using System.Text;
using Newtonsoft.Json.Linq;
using Henry.Common;
namespace FrontSite.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Switch(string basekey)
        {
          return RedirectToAction("CategoryList");
        }
        /// <summary>
        /// 文章类别列表
        /// </summary>
        /// <param name="basekey">顶级类别code</param>
        /// <param name="smallkey">小分类code</param>
        /// <returns></returns>
        public ActionResult CategoryList(string basekey,string smallkey)
        {
            var categories = ArticleCategoryBLL.GetItemsByCode(basekey);
            return View(categories);
        }
        public ActionResult ArticleList(int id)
        {
            var entity = new Models.Chapter();
            entity.ArticleItems = ArticleBLL.GetList(new Henry.Entity.Article { A_CategoryID = id });
            entity.CategoryInfo = ArticleCategoryBLL.GetSingleOrDefault(id);
            return View(entity);
        }
        public ActionResult ArticleList1(int id)
        {
            return View();
        }
        public ActionResult ArticleDetail(int id)
        {
            var articleEntity = ArticleBLL.GetSingleOrDefault(id);
            return View(articleEntity);
        }
        [HttpPost]
        public NewtonJsonResult GetCategoryList(string key, int page, int pagesize)
        {
            var objData = new JObject();
            var result = ArticleCategoryBLL.GetShowItemsByCode(key,page,pagesize);
            var itemArray = new JArray();
            JObject tempObjData;
            foreach (var i in result.Items)
            {
                tempObjData = new JObject();
                tempObjData["name"] = i.AC_Name;
                tempObjData["id"] = i.AC_ID;
                tempObjData["desc"] = i.AC_Description;
                tempObjData["readtimes"] = i.TotalReadTimes;
                tempObjData["image"] = i.AC_PicName;
                tempObjData["complete"] = i.AC_IsComplete;
                tempObjData["lasttime"] = i.LastPostTime.FormatDate();
                tempObjData["itemcount"] = i.TotalArticles;
                itemArray.Add(tempObjData);
            }
            objData["items"] = itemArray;
            objData["total"] = result.TotalCount;
            return new NewtonJsonResult { Data=objData};
        }
    }
}
