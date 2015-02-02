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

    public class PhotoController : Controller
    {
        [HttpPost]
        public NewtonJsonResult List(int pageindex, int pagesize, int category)
        {
            var resultObj = new AjaxGetDataResult();
            var articleInfo = new Photo() { PageIndex = pageindex, PageSize = pagesize, P_CategoryID = category };
            var pageData = PhotoBLL.GetListWithPage(articleInfo);
            resultObj.total = pageData.TotalCount;
            resultObj.pagesize = pageData.PageSize;
            resultObj.currentpage = pageData.PageIndex;
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in pageData.Items)
            {
                temp = new JObject();
                temp["id"] = item.P_ID;
                temp["filename"] = item.P_FileName;
                temp["tagname"] = item.P_TagName;
                temp["status"] = item.P_Status;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            return new NewtonJsonResult() { Data = resultObj };
        }

        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(Photo info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.P_ID > 0) { result = PhotoBLL.Update(info); }
            else { result = PhotoBLL.Add(info); add = true; }

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
                result = PhotoBLL.Delete(id);
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
