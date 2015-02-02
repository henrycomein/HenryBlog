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

    public class MenuController : Controller
    {
        //
        // GET: /ArticleAPI/
        [HttpPost]
        public NewtonJsonResult List()
        {
            var resultObj = new AjaxGetDataResult();
            var lstData = MenusBLL.GetList(new Menus() { });
            var itemArray = new JArray();

            JObject temp;
            foreach (var item in lstData)
            {
                temp = new JObject();
                temp["id"] = item.M_ID;
                temp["name"] = item.M_Name;
                temp["pid"] = item.M_ParentID;
                temp["link"] = item.M_Url;
                temp["fullpname"] = item.M_ParentFullName;
                temp["sort"] = item.M_OrderIndex;
                temp["status"] = item.M_Status;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            resultObj.total = lstData.Count;
            return new NewtonJsonResult() { Data = resultObj };
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(Menus info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.M_ID > 0) { result = MenusBLL.Update(info); }
            else { result = MenusBLL.Add(info); add = true; }

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
                result = ArticleCategoryBLL.Delete(id);
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
        /// <summary>
        /// 用于菜单数据呈现
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 当无id传入的时候（id小于等于0），捞出所有数据
        /// 当有id传入的时候（id大于0），该类别所有同级以及上级节点，并且不包含自身
        /// </returns>
        public NewtonJsonResult Tree(int id=0)
        {

            var resultObj = new AjaxGetDataResult();
            var data = MenusBLL.GetTree(id);
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in data)
            {
                temp = new JObject();
                temp["id"] = item.M_ID;
                temp["name"] = item.M_Name;
                temp["pid"] = item.M_ParentID;
                temp["sort"] = item.M_OrderIndex;
                temp["fullpname"] = item.M_ParentFullName;
                temp["status"] = item.M_Status;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            resultObj.total = data.Count;
            return new NewtonJsonResult() { Data = resultObj };
        }
    }
}
