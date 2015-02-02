using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manage.Controllers
{
    public class AjaxGetDataResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public Newtonsoft.Json.Linq.JArray items { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int pagesize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int currentpage { get; set; }

    }
}