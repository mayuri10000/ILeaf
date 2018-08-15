using ILeaf.Core.Utilities;
using StructureMap;
using System;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public static string StrcutureMapStartupTime;
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                {
                    UrlHelper urlHelper = new UrlHelper(requestContext);
                    string url = urlHelper.Action("Error404", "Error", new { area = "", aspxerrorpath = requestContext.HttpContext.Request.Url.PathAndQuery });
                    HttpContext.Current.Response.Redirect(url);
                }
                if (!typeof(IController).IsAssignableFrom(controllerType))
                {
                    string msg = string.Format(
                        "Type requested is not a controller: {0}",
                        controllerType == null ? "" : controllerType.Name);
                    Logger.WebLogger.ErrorFormat("{0}，{1}", requestContext.HttpContext.Request.Url, msg);
                    throw new ArgumentException(msg,
                           "controllerType");
                }

                return ObjectFactory.GetInstance(controllerType) as IController;
            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }
        }
    }
}