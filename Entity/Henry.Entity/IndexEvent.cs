using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Entity
{
    public class IndexEvent
    {
        public List<LifeEventCategory> CategoryData { get; set; }
        public List<MonthData> MonthData { get; set; }
        public List<YearData> YearData { get; set; }
        public List<LifeEvent> MonthEvent { get; set; }
    }
    public class MonthData
    {
        public int Month { get; set; }
    }
     public class YearData
    {
        public int Year { get; set; }
    }
}
