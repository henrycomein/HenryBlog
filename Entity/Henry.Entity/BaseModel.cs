using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Entity
{
    public class BaseModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int ItemTotalCount { get; set; }
        public string OrderBy { get; set; } 
    }
}
