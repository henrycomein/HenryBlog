using Henry.Entity;
using Henry.Manage.BusinessLayer;
using Manage.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manage.Controllers.API
{
    
    public class ArticleController : Controller
    {
        //
        // GET: /ArticleAPI/
        [HttpPost]
        public NewtonJsonResult List(int pageindex, int pagesize, string content, int category = 0)
        {
            var resultObj = new AjaxGetDataResult();
            var articleInfo = new Article() { PageIndex = pageindex, PageSize = pagesize, A_Content = content, A_CategoryID = category };
            var pageData = ArticleBLL.GetListWithPage(articleInfo);
            resultObj.total = pageData.TotalCount;
            resultObj.pagesize = pageData.PageSize;
            resultObj.currentpage = pageData.PageIndex;
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in pageData.Items)
            {
                temp = new JObject();
                temp["id"] = item.A_ID;
                temp["title"] = item.A_Title;
                temp["category"] = item.A_CategoryName;
                temp["istop"] = item.A_IsTop;
                temp["sort"] = item.A_Sort;
                temp["status"] = item.A_Status;
                temp["time"] = item.A_CreateTime;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            return new NewtonJsonResult() { Data = resultObj };
        }
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(Article info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.A_ID > 0) { result = ArticleBLL.Update(info); }
            else { result = ArticleBLL.Add(info); add = true; }

            resultObj.success = result ? 1 : 0;
            resultObj.message = add ? result ? "新增成功！" : "新增失败！" : result ? "保存成功！" : "保存失败！";
            return new NewtonJsonResult() { Data = resultObj };
        }
        [HttpPost]
        public NewtonJsonResult Delete(int id) {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            if (id > 0)
            {
                result = ArticleBLL.Delete(id);
                resultObj.message = result ? "删除成功！" : "删除失败！";
            }
            else
            {
                resultObj.message = "操作非法！";
            }
            resultObj.success = result ? 1 : 0;
            return new NewtonJsonResult() { Data = resultObj };
        }
    }
}
