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

    public class PhotoCategoryController : Controller
    {
        
        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(PhotoCategory info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            if (info.PC_ID > 0) { result = PhotoCategoryBLL.Update(info); }
            else { result = PhotoCategoryBLL.Add(info); add = true; }

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
                result = PhotoCategoryBLL.Delete(id);
                resultObj.message = result ? "删除成功！" : "删除失败！";
            }
            else
            {
                resultObj.message = "操作非法！";
            }
            resultObj.success = result ? 1 : 0;
            return new NewtonJsonResult() { Data = resultObj };
        }
        /// <summary>
        /// 设置封面
        /// </summary>
        /// <param name="id">类别id</param>
        /// <param name="pid">照片id</param>
        /// <returns></returns>
        [HttpPost]
        public NewtonJsonResult SetCover(int id,int pid)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            if (id > 0)
            {
                result = PhotoCategoryBLL.SetCover(pid,id);
                resultObj.message = result ? "设置成功！" : "设置失败！";
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
