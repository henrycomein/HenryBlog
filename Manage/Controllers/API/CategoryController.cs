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

    public class CategoryController : Controller
    {
        //
        // GET: /ArticleAPI/
        [HttpPost]
        public NewtonJsonResult List()
        {
            var resultObj = new AjaxGetDataResult();
            var pageData = ArticleCategoryBLL.GetTreeList();
            var itemArray = new JArray();

            GenerateLevelData(0, pageData, ref itemArray);
            resultObj.items = itemArray;
            resultObj.total = pageData.Count;
            return new NewtonJsonResult() { Data = resultObj };
        }
        private void GenerateLevelData(int parentid,List<ArticleCategory> allcategory, ref JArray itemArray)
        {
            var itemCategories = allcategory.Where(i => i.AC_ParentID == parentid);
            JObject temp;
            foreach (var item in itemCategories)
            {
                temp = new JObject();
                temp["id"] = item.AC_ID;
                temp["name"] = item.AC_Name;
                temp["pid"] = item.AC_ParentID;
                temp["code"] = item.AC_Code;
                temp["show"] = item.AC_ShowFront;
                temp["ismin"] = item.AC_ShowList;
                temp["sort"] = item.AC_Sort;
                temp["status"] = item.AC_Status;
                itemArray.Add(temp);
                GenerateLevelData(item.AC_ID, allcategory,ref itemArray);
            }
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(ArticleCategory info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.AC_ID > 0) { result = ArticleCategoryBLL.Update(info); }
            else { result = ArticleCategoryBLL.Add(info); add = true; }

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
        /// 用于类别数呈现
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 当无id传入的时候（id小于等于0），捞出所有数据
        /// 当有id传入的时候（id大于0），该类别所有同级以及上级节点，并且不包含自身
        /// </returns>
        public NewtonJsonResult Tree(int id=0)
        {

            var resultObj = new AjaxGetDataResult();
            var data = ArticleCategoryBLL.GetTree(id);
            var itemArray = new JArray();
            JObject temp;
            foreach (var item in data)
            {
                temp = new JObject();
                temp["id"] = item.AC_ID;
                temp["name"] = item.AC_Name;
                temp["pid"] = item.AC_ParentID;
                temp["nocheck"] = id > 0 ? false : item.AC_ShowList == 0;
                itemArray.Add(temp);
            }
            resultObj.items = itemArray;
            resultObj.total = data.Count;
            return new NewtonJsonResult() { Data = resultObj };
        }
    }
}
