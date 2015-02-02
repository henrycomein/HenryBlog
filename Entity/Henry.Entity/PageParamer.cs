using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Entity
{
    public class PageParamer<T>
    {
        /// <summary>
        /// 查询返回对象
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
    }
}
