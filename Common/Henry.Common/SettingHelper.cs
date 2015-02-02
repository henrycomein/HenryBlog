using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Henry.Common
{
    public class SettingHelper
    {
        private static readonly Dictionary<SettingName, string> settings;
        static SettingHelper()
        {
            settings = new Dictionary<SettingName, string>();
            settings.Add(SettingName.DbConnectString, ConfigurationManager.AppSettings["DbConnectString"]);
            settings.Add(SettingName.ImageBasicPath, ConfigurationManager.AppSettings["ImageBasicPath"]);
            settings.Add(SettingName.UploadImagePath, ConfigurationManager.AppSettings["UploadImagePath"]);
        }
        public static string DbConnnectString()
        {
            return settings[SettingName.DbConnectString];
        }
        public static string ImageVisitePath()
        {
            return settings[SettingName.ImageBasicPath] + "image/";
        }
        public static string PhotoVisitePath()
        {
            return settings[SettingName.ImageBasicPath] + "photo/";
        }
        public static string UploadImagePath()
        {
            return settings[SettingName.UploadImagePath] + "image\\";
        }
        public static string UploadPhotoPath()
        {
            return settings[SettingName.UploadImagePath] + "photo\\";
        }
    }
    public enum SettingName
    {
        DbConnectString,
        ImageBasicPath,
        UploadImagePath,
        UploadPhotoPath
    }
}
