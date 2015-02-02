using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Henry.Common
{
    public class LoggerHelper
    {
        private static NLog.Logger log = NLog.LogManager.GetLogger("toFile");
        private static NLog.Logger dbLog=NLog.LogManager.GetLogger("toDb");

#region 写入文本文件
        public static void Trace(string msg)
        {
            log.Trace(msg);
        }
        public static void Info(string msg)
        {
            log.Info(msg);
        }
        public static void Warn(string msg)
        {
            log.Warn(msg);
        }
        public static void Warn(Exception error)
        {
            log.Warn(error);
        }
        public static void Error(string msg)
        {
            log.Error(msg);
        }
        public static void Error(Exception error)
        {
            log.Error(error);
        }
#endregion

#region 写入数据库db
        public static void DbInfo(string msg)
        {
            dbLog.Info(msg);
        }
        public static void DbInfo(Exception error)
        {
            dbLog.Info(error);
        }
        public static void DbWarn(string msg)
        {
            dbLog.Warn(msg);
        }
        public static void DbWarn(Exception error)
        {
            dbLog.Warn(error);
        }
        public static void DbError(string msg)
        {
            dbLog.Error(msg);
        }
        public static void DbError(Exception error)
        {
            dbLog.Error(error);
        }
        public static void DbFatal(string msg)
        {
            dbLog.Fatal(msg);
        }
        public static void DbFatal(Exception error)
        {
            dbLog.Fatal(error);
        }
#endregion
    }
}
