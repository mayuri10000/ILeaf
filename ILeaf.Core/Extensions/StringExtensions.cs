/*  StringExtensions.cs  字符串扩展方法
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

using System;

namespace ILeaf.Core.Extensions
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            return string.IsNullOrEmpty(str);
        }

        public static T ShowWhenNullOrEmpty<T>(this T obj, T defaultConent)
        {
            if (obj == null)
            {
                return defaultConent;
            }
            else if (obj is String && obj.ToString() == "")
            {
                return defaultConent;
            }
            else
            {
                return obj;
            }
        }

        public static string UrlEncode(this string str)
        {
            return System.Web.HttpUtility.UrlEncode(str);
        }

        public static string UrlDecode(this string str)
        {
            return System.Web.HttpUtility.UrlDecode(str);
        }

        

    }
}
