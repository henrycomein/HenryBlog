using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Henry.Common
{
    /// <summary>
    /// 字符串辅助类
    /// </summary>
    public static partial class StringHelper
    {
        public const string nullStr = "NULL";

        #region Sql字符串验证相关

        /// <summary>
        /// 检验字符串变量
        /// </summary>
        /// <param name="sql">字符变量</param>
        /// <returns></returns>
        public static string CheckSqlStrParamer(this string sql)
        {
            if (sql == null)
                return string.Empty;
            return sql.Replace("'", "''");
        }
        /// <summary>
        /// 检验时间变量
        /// </summary>
        /// <param name="date">时间变量</param>
        /// <returns></returns>
        public static string CheckSqlDateParamer(this DateTime date)
        {
            if (date == null)
                return nullStr;
            return FormatDateTime(date);
        }

        /// <summary>
        /// 检验xml变量
        /// </summary>
        /// <param name="xmldata">xml变量</param>
        /// <returns></returns>
        public static string CheckSqlXmlParamer(this string xmldata)
        {
            if (xmldata == null)
                return string.Empty;
            return xmldata.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        #endregion

        #region 时间格式转换

        /// <summary>
        /// 转换成短时间格式
        /// </summary>
        /// <param name="date">时间数据源</param>
        /// <returns>yyyy-MM</returns>
        public static string FormatShortDate(this DateTime date)
        {
            var result = string.Empty;
            if (date != null)
            {
                result = date.ToString("yyyy-MM");
            }
            return result;
        }

        /// <summary>
        /// 转换成一般时间格式
        /// </summary>
        /// <param name="date">时间数据源</param>
        /// <returns>yyyy-MM-dd</returns>
        public static string FormatDate(this DateTime date)
        {
            var result = string.Empty;
            if (date != null)
            {
                result = date.ToString("yyyy-MM-dd");
            }
            return result;
        }
        /// <summary>
        /// 转换成一般时间格式
        /// </summary>
        /// <param name="date">时间数据源</param>
        /// <returns>yyyy-MM-dd  HH:mm:ss</returns>
        public static string FormatDateTime(this DateTime date)
        {
            var result = string.Empty;
            if (date != null)
            {
                result = date.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        public static string FormatDateTimeMinute(this DateTime date)
        {
            var result = string.Empty;
            if (date != null)
            {
                result = date.ToString("yyyy-MM-dd HH:mm");
            }
            return result;
        }
#endregion

        #region 一般字符串操作类


        /// <summary>
        /// 踢出HTML
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);", 
            @"&(nbsp|#160);", 
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };
            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region  加密解密
        public static string GetMD5(string encyptStr)
        {
            if (encyptStr == null || encyptStr.Length == 0)
                return string.Empty;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();
            byte[]  inputBye = System.Text.Encoding.ASCII.GetBytes(encyptStr);
            byte[]  outputBye = m5.ComputeHash(inputBye);
            return Convert.ToBase64String(outputBye);
        }
        #endregion
    }
}
