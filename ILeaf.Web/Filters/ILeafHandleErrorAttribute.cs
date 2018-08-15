using ILeaf.Core.Utilities;
using ILeaf.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Filters
{
    public class ILeafHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            //Record Log
            //LogUtility.WebLogger.Error(
            //    string.Format("SenparcHandle错误。IP：{0}。页面：{1}",
            //        filterContext.HttpContext.Request.UserHostName,
            //        filterContext.HttpContext.Request.Url.PathAndQuery),
            //    filterContext.Exception);

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            Exception exception = filterContext.Exception;

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }

            string controllerName = (string)filterContext.RouteData.Values["controller"];
            string actionName = (string)filterContext.RouteData.Values["action"];
            ExceptionViewModel vd = new ExceptionViewModel
            {
                HandleErrorInfo = new HandleErrorInfo(filterContext.Exception, controllerName, actionName)
            };
            filterContext.Result = new ViewResult
            {
                ViewName = View,
                MasterName = Master,
                ViewData = new ViewDataDictionary<ExceptionViewModel>(vd),
                TempData = filterContext.Controller.TempData
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;

            Logger.WebLogger.Error("500错误:" + exception.Message, exception);

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}