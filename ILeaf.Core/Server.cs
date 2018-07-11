/*  Server.cs  服务器静态类，用于数据访问层、业务逻辑层代码获取HttpContext及获取服务器文件路径
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月8日
 */
using System.Web;
using System.IO;

namespace ILeaf.Core
{
    /// <summary>
    /// 服务器静态类，用于数据访问层、业务逻辑层代码获取HttpContext及获取服务器文件路径
    /// </summary>
    public static class Server
    {
        private static string _appDomainAppPath;
        /// <summary>
        /// 当前服务器应用路径
        /// </summary>
        public static string AppDomainAppPath
        {
            get
            {
                if (_appDomainAppPath == null)
                {
                    _appDomainAppPath = HttpRuntime.AppDomainAppPath;
                }
                return _appDomainAppPath;
            }
            set
            {
                _appDomainAppPath = value;
            }
        }

        /// <summary>
        /// 根据虚拟路径获取服务器上的文件的绝对路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns></returns>
        public static string GetMapPath(string virtualPath)
        {
            if (virtualPath == null)
            {
                return "";
            }
            else if (virtualPath.StartsWith("~/"))
            {
                return virtualPath.Replace("~/", AppDomainAppPath).Replace("/", "\\");
            }
            else
            {
                return Path.Combine(AppDomainAppPath, virtualPath.Replace("/", "\\"));
            }
        }

        /// <summary>
        /// 获取当前HttpContext对象
        /// </summary>
        public static HttpContext HttpContext
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                {
                    HttpRequest request = new HttpRequest("Default.aspx", "http://www.senparc.com/default.aspx", null);
                    StringWriter sw = new StringWriter();
                    HttpResponse response = new HttpResponse(sw);
                    context = new HttpContext(request, response);
                }
                return context;
            }
        }
    }
}
