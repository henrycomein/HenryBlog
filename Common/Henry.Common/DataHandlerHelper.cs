using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Henry.Common
{
    public static class DataHandlerHelper
    {
        public static List<T> ToEntity<T>(this DataTable dt)
        {
            var items = new List<T>();
            Type t =typeof(T);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    var datacount = dt.Rows.Count;
                    for (int i = 0; i < datacount; i++)
                    {
                        T item = Activator.CreateInstance<T>();
                        var properties=t.GetProperties();
                        foreach (var p in properties)
                        {
                            if (dt.Columns.Contains(p.Name))
                            {
                                if (dt.Rows[i][p.Name] != DBNull.Value)
                                {
                                    //p.SetValue(item, dt.Rows[i][p.Name],null);
                                    p.SetValue(item, Convert.ChangeType(dt.Rows[i][p.Name], p.PropertyType), null);
                                }
                            }
                        }
                        items.Add(item);
                    }
                }
            }
            return items;
        }
    }
}
