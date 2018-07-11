/*  SiteConfig.cs  网站配置类，用于访问配置信息
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace ILeaf.Core.Config
{
    public class SiteConfig
    {
        private static string _applicationPath;
        /// <summary>
        /// 应用程序路径
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                if (_applicationPath == null)
                {
                    string path = Server.HttpContext.Request.ApplicationPath;
                    if (path.EndsWith("/"))
                    {
                        _applicationPath = path.Substring(0, path.Length - 1);
                    }
                    else
                    {
                        _applicationPath = path;
                    }
                }
                return _applicationPath;
            }
        }

        public readonly static string VERSION = "1.3.3";
        public const string DEFAULT_AVATAR = "Default_Avator.jpg";           //默认头像地址
        /// <summary>
        /// 域名http://xx.xxx.com
        /// </summary>
        public static string DomainName
        {
            get { return ConfigurationManager.AppSettings["DomainName"]; }
        }
        /// <summary>
        /// AppId
        /// </summary>
        public static string AppId
        {
            get { return ConfigurationManager.AppSettings["AppId"]; }
        }
        /// <summary>
        /// AppSecret
        /// </summary>
        public static string AppSecret
        {
            get { return ConfigurationManager.AppSettings["AppSecret"]; }
        }

        /// <summary>
        /// 用户密码加密公钥 (RSA-512)
        /// </summary>
        public static string EncryptKey
        {
            get { return ConfigurationManager.AppSettings["EncryptKey"]; }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string DbConnectionString
        {
            get { return ConfigurationManager.AppSettings["DbConnectionString"]; }
        }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public static string EmailSourceAddress
        {
            get { return ConfigurationManager.AppSettings["EmailSourceAddress"]; }
        }

        /// <summary>
        /// 邮件服务器
        /// </summary>
        public static string EmailServer
        {
            get { return ConfigurationManager.AppSettings["EmailServer"]; }
        }

        /// <summary>
        /// 邮箱登陆用户名
        /// </summary>
        public static string EmailUserName
        {
            get { return ConfigurationManager.AppSettings["EmailUserName"]; }
        }

        /// <summary>
        /// 邮箱登陆密码
        /// </summary>
        public static string EmailPassword
        {
            get { return ConfigurationManager.AppSettings["EmailPassword"]; }
        }

        /// <summary>
        /// 用户登录有效时间（单位：小时）
        /// </summary>
        public static int UserExpires
        {
            get
            {
                int expires = 1;
                int.TryParse(ConfigurationManager.AppSettings["UserExpires"], out expires);
                return expires;
            }
        }
        
        /// <summary>
        /// 最大数据库备份文件个数
        /// </summary>
        public static readonly int MaxBackupDatabaseCount = 200;

        /// <summary>
        /// 最多免验证码尝试登录次数
        /// </summary>
        public static readonly int TryUserLoginTimes = 3;

        ///// <summary>
        ///// 自动短信到期提示时间点（天）
        ///// </summary>
        //public static int[] SmsExpireAlertLastDays = new int[] { 30, 10, 3, 1 };//提醒日期

        //统计数据
        public static DateTime ApplicationStartTime = DateTime.Now;
        public static int PageViewCount { get; set; }//网站启动后前台页面浏览量

        //异步线程
        public static Dictionary<string, Thread> AsynThread = new Dictionary<string, Thread>();//后台运行线程

        private static bool isDebug = false;
        private static bool isDebugChecked = false;

        /// <summary>
        /// 判断是否是测试状态，如果测试状态为true，则强制认为IsDebug=true;
        /// </summary>
        public static bool IsTest = false;
        public static bool IsDebug
        {
            get
            {
                if (IsTest)
                {
                    return true;
                }

                //每次都重新判断
                if (!isDebugChecked)
                {
                    isDebug = ConfigurationManager.AppSettings["SystemIsDebug"] == "true"
                                      //HttpContext context = Server.HttpContext;
                                      //isDebug = (context.IsDebuggingEnabled && context.Request.IsLocal)
                                      //              || context.Request.Url.Host.ToUpper().Contains("LOCAL")
                                      //              || context.Request.Url.HostNameType == UriHostNameType.IPv4
                                      //    //       (!ConfigurationManager.ConnectionStrings["SenparcEntities"].ConnectionString.Contains("a0420205424"))
                                      ;
                    isDebugChecked = true;
                }
                return isDebug;
            }
        }

        //static readonly string _cacheTypeString = WebConfigurationManager.AppSettings["CacheType"];
        //private static CacheType? _cacheType;

        /// <summary>
        /// 缓存类型
        /// </summary>
        //public static CacheType CacheType
        //{
        //    get
        //    {
        //        if (_cacheType == null)
        //        {
        //            if (_cacheTypeString.IsNullOrEmpty())
        //            {
        //                _cacheType = CacheType.Location;
        //            }
        //            else
        //            {
        //                switch (_cacheTypeString.ToUpper())
        //                {
        //                    case "MEMCACHED":
        //                        _cacheType = CacheType.Memcached;
        //                        break;
        //                    case "REDIS":
        //                        _cacheType = File.Exists(Server.GetMapPath("~/App_Data/UseRedis.txt"))
        //                            ? CacheType.Redis //必须文件存在才会启用
        //                            : CacheType.Location;
        //                        break;
        //                    default:
        //                        _cacheType = CacheType.Location;
        //                        break;
        //                }
        //            }
        //        }

        //        return _cacheType.Value;
        //    }
        //    set { _cacheType = value; }
        //}
    }
}
