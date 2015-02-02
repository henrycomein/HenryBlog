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
    public class AboutMeController : Controller
    {
        //
        // GET: /Event/

        [HttpPost]
        [ValidateInput(false)]
        public NewtonJsonResult Update(AboutMe info)
        {
            var resultObj = new AjaxHandleDataResult();
            var result = false;
            var add = false;
            result = AboutMeBLL.Update(info); 

            resultObj.success = result ? 1 : 0;
            resultObj.message = add ? result ? "新增成功！" : "新增失败！" : result ? "保存成功！" : "保存失败！";
            return new NewtonJsonResult() { Data = resultObj };
        }

    }
}
