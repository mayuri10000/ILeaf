using ILeaf.Core.Enums;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        public UserType RequiredUserType = UserType.Unregistered;

        public AuthAttribute() { }

        public AuthAttribute(UserType requiredUserType)
        {
            this.RequiredUserType = requiredUserType;
        }

        public virtual bool Authorize(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;
            else if (RequiredUserType != UserType.Unregistered 
                && (UserType)httpContext.Session["UserType"] != RequiredUserType)
                return false;

            return true;
        }

        // This method must be thread-safe since it is called by the caching module.
        protected virtual HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            bool isAuthorized = Authorize(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (Authorize(filterContext.HttpContext))
            {
                // ** IMPORTANT **
                // Since we're performing authorization at the action level, the authorization code runs
                // after the output caching module. In the worst case this could allow an authorized user
                // to cause the page to be cached, then an unauthorized user would later be served the
                // cached page. We work around this by telling proxies not to cache the sensitive page,
                // then we hook our custom authorization code into the caching mechanism so that we have
                // the final say on whether a page should be served from the cache.

                HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
            }
            else
            {
                // auth failed, redirect to login page

                //if (filterContext.RouteData.DataTokens["area"] != null
                //    && filterContext.RouteData.DataTokens["area"].ToString().ToUpper() == "ADMIN")
                //{
                //    filterContext.Controller.TempData["AdminLogin"] = true;
                //}

                //todo: to a special page
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}