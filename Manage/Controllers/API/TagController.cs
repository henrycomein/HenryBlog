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

    public class TagController : Controller
    {
        //
        // GET: /ArticleAPI/
        [HttpPost]
        public NewtonJsonResult List(int pageindex, int pagesize, string content, int category = 0)
        {
            var resultObj = new AjaxGetDataResult();
            var info = new Tag() { PageIndex = pageindex, PageSize = pagesize};
            var pageData = TagBLL.GetListWithPage(info);
            resultObj.total = pageData.TotalCount;
            resultObj.pagesize = pageData.PageSize;
            resultObj.currentpage = pageData.PageIndex;
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in pageData.Items)
            {
                temp = new JObject();
                temp["id"] = item.T_ID;
                temp["name"] = item.T_Name;
                temp["sort"] = item.T_Sort;
                temp["isphoto"] = item.T_IsPhoto;
                temp["status"] = item.T_Status;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            return new NewtonJsonResult() { Data = resultObj };
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(Tag info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.T_ID > 0) { result = TagBLL.Update(info); }
            else { result = TagBLL.Add(info); add = true; }

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
                result = TagBLL.Delete(id);
                resultObj.message = result ? "删除成功！" : "删除失败！";
            }
            else
            {
                resultObj.message = "操作非法！";
            }
            resultObj.success = result ? 1 : 0;
            return new NewtonJsonResult() { Data = resultObj };
        }
        [HttpPost]
        public NewtonJsonResult Tree(int isphoto=0)
        {

            var resultObj = new AjaxGetDataResult();
            var info = new Tag() { T_Status = 1, T_IsPhoto = isphoto };
            var data = TagBLL.GetList(info);
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in data)
            {
                temp = new JObject();
                temp["id"] = item.T_ID;
                temp["name"] = item.T_Name;
                temp["sort"] = item.T_Sort;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            resultObj.total = data.Count;
            return new NewtonJsonResult() { Data = resultObj };
        }
    }
}
