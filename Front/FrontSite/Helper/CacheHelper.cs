using System;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Caching;

namespace Henry.Common
{

    public enum CacheItem
    {
        TodayPostArticleRank,
        YestodayPostArticleRank,
        Category,
        FaceImage
    }
    public class CacheHelper
    {
        private static string optionPath = HttpContext.Current.Server.MapPath("~/App_Data/CacheExpirationOption.xml");

        public static string GetOptionPath()
        {
            return optionPath;
        }
        public static int ExpiredTime(CacheItem citem)
        {
            int time = 0;
            string name = citem.ToString();
            XElement element = XElement.Load(optionPath);
            if (element != null)
            {
                var item = element.Elements("Item").FirstOrDefault(i => i.Attribute("name").Value == name);
                if (item != null)
                {
                    int.TryParse(item.Element("ExpirationTime").Value, out time);
                }
            }
            return time;

        }
        public static object Get(CacheItem citem)
        {
            string key=citem.ToString();
            return HttpRuntime.Cache.Get(key);
        }
        public static void Add(CacheItem citem, object value)
        {
            string key = citem.ToString();
            HttpRuntime.Cache.Insert(key, value);
        }
        public static void Add(CacheItem citem, object value, CacheDependency dependency)
        {
            string key = citem.ToString();
            HttpRuntime.Cache.Insert(key, value, dependency);
        }
        public static void Add(CacheItem citem, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            string key = citem.ToString();
            HttpRuntime.Cache.Insert(key, value, dependency, absoluteExpiration, slidingExpiration);
        }
        public static void Add(CacheItem citem, object value, CacheDependency dependency, DateTime absoluteExpiration, TimeSpan slidingExpiration,CacheItemPriority priority)
        {
            string key = citem.ToString();
            HttpRuntime.Cache.Insert(key, value, dependency, absoluteExpiration, slidingExpiration, priority,null);
        }
    }
}