using ILeaf.Core.Utilities;
using StructureMap;
using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ILeaf.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Log4net
            {
                DateTime st = DateTime.Now;
                var log4netFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                var log4netFileInfo = new System.IO.FileInfo(log4netFilePath);
                log4net.Config.XmlConfigurator.Configure(log4netFileInfo);
                DateTime et = DateTime.Now;
                Logger.WebLogger.InfoFormat("系统启动，log4net初始化启动时间：{0}秒", (et - st).TotalSeconds);
            }

            //IoC
            {
                DateTime st = DateTime.Now;
                ObjectFactory.Initialize(x=> 
                {
                    x.Scan(y =>
                    {
                        y.Assembly("ILeaf.Core");
                        y.Assembly("ILeaf.Repository");
                        y.Assembly("ILeaf.Service");
                        y.WithDefaultConventions();
                    });
                });
                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
                DateTime et = DateTime.Now;
                StructureMapControllerFactory.StrcutureMapStartupTime = (et - st).TotalSeconds.ToString("##,##");
            }

            //Default controller
            ControllerBuilder.Current.DefaultNamespaces.Add("ILeaf.Web.Controllers");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
