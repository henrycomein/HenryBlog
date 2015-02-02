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
    public class EventController : Controller
    {
        //
        // GET: /Event/

        [HttpPost]
        public NewtonJsonResult List(int pageindex, int pagesize, string content, int category = 0)
        {
            var resultObj = new AjaxGetDataResult();
            var eventInfo = new LifeEvent() { PageIndex = pageindex, PageSize = pagesize, LE_Desc = content};
            var pageData = LifeEventBLL.GetListWithPage(eventInfo);
            resultObj.total = pageData.TotalCount;
            resultObj.pagesize = pageData.PageSize;
            resultObj.currentpage = pageData.PageIndex;
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in pageData.Items)
            {
                temp = new JObject();
                temp["id"] = item.LE_ID;
                temp["title"] = item.LE_Title;
                temp["date"] = Henry.Common.StringHelper.FormatDateTime(item.LE_Date);
                temp["status"] = item.LE_Status;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            return new NewtonJsonResult() { Data = resultObj };
        }
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(LifeEvent info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.LE_ID > 0) { result = LifeEventBLL.Update(info); }
            else { result = LifeEventBLL.Add(info); add = true; }

            resultObj.success = result ? 1 : 0;
            resultObj.message = add ? result ? "新增成功！" : "新增失败！" : result ? "保存成功！" : "保存失败！";
            return new NewtonJsonResult() { Data = resultObj };
        }
        [HttpPost]
        public NewtonJsonResult Delete(int id)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            if (id > 0)
            {
                result = LifeEventBLL.Delete(id);
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
