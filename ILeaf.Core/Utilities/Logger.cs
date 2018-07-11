/*  Logger.cs  日志静态类，用于网站日志的记录
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

namespace ILeaf.Core.Utilities
{
    public static class Logger
    {
        public static object LogLock = new object();

        public static log4net.ILog SystemLogger { get { return GetLogger("SystemLogger"); } }
        public static log4net.ILog WebLogger { get { return GetLogger("WebLogger"); } }
        public static log4net.ILog Account { get { return GetLogger("Account"); } }
        public static log4net.ILog DebugLogger { get { return GetLogger("DebugLogger"); } }
        public static log4net.ILog OAuthLogger { get { return GetLogger("OAuthLogger"); } }
        public static log4net.ILog Weixin { get { return GetLogger("Weixin"); } }
        
        public static log4net.ILog GetLogger(string name)
        {
            lock (Logger.LogLock)
            {
                return log4net.LogManager.GetLogger(name);
            }
        }
    }
}
