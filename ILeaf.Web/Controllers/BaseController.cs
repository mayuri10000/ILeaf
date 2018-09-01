using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Service;
using ILeaf.Web.Filters;
using ILeaf.Web.Models;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ILeaf.Web.Controllers
{
    [ILeafHandleError]
    public class BaseController : AsyncController
    {
        //private ISystemConfigService _systemConfigService;
        protected DateTime PageStartTime { get; set; }
        protected DateTime PageEndTime { get; set; }
        

        public BaseController()
        {
            PageStartTime = DateTime.Now;
        }

        public Account Account
        {
            get => Session["Account"] as Account;
        }

        public string UserName
        {
            get
            {
                return User.Identity.IsAuthenticated ? User.Identity.Name : null;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return ((Account)Session["Account"]).IsAdmin;
            }
        }



        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            if (filterContext.Controller.ViewData.Model is BaseViewModel)
            {
                var vd = filterContext.Controller.ViewData.Model as BaseViewModel;
                vd.RouteData = this.RouteData;
                vd.Account = Account;

                vd.MessagerList = vd.MessagerList ?? new List<Messager>();
                if (TempData["Messager"] as List<Messager> != null)
                {

                    vd.MessagerList.AddRange(TempData["Messager"] as List<Messager>);
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        vd.MessagerList.Add(new Messager(MessageLevel.Error, "提交信息有错误，请检查。"));
                    }
                }
            }

            if (filterContext.Controller.ViewData.Model is BaseViewModel)
            {
                var vd = filterContext.Controller.ViewData.Model as BaseViewModel;
                PageEndTime = DateTime.Now;
                vd.PageStartTime = PageStartTime;
                vd.PageEndTime = PageEndTime;
            }

            base.OnResultExecuting(filterContext);
        }
        

        protected override void HandleUnknownAction(string actionName)
        {
            string url = Url.Action("Error404", "Error", new { aspxerrorpath = Request.Url.PathAndQuery/*, t = DateTime.Now.Ticks*/ });
            this.Response.Redirect(url, true);
            //base.HandleUnknownAction(actionName);
        }

        #region 成功或错误提示页面

        [NonAction]
        public virtual ActionResult RenderSuccess(string message, string backUrl = null, string backAction = null, string backController = null, RouteValueDictionary backRouteValues = null, SuccessViewModel vd = null)
        {
            if (backUrl.IsNullOrEmpty())
            {
                backAction = backAction ?? RouteData.GetRequiredString("action");
                backController = backController ?? RouteData.GetRequiredString("controller");
                backRouteValues = backRouteValues ?? new RouteValueDictionary();

            }
            vd = vd ?? new SuccessViewModel()
            {
                Message = message,
                BackUrl = backUrl,
                BackAction = backAction,
                BackController = backController,
                BackRouteValues = backRouteValues
            };
            return View("Success", vd);
        }

        [NonAction]
        public virtual ActionResult RenderUnauthorized()
        {
            TempData["AuthorityNotReach"] = "无权操作，请联系管理员！";
            return new HttpUnauthorizedResult();
        }

        [NonAction]
        public virtual ActionResult RenderError(string message)
        {
            //保留原有的controller和action信息
            ViewData["FakeControllerName"] = RouteData.GetRequiredString("controller");
            ViewData["FakeActionName"] = RouteData.GetRequiredString("action");

            return View("Error", new ExceptionViewModel
            {
                HandleErrorInfo = new HandleErrorInfo(new Exception(message), Url.RequestContext.RouteData.GetRequiredString("controller"), Url.RequestContext.RouteData.GetRequiredString("action"))
            });
        }
        [NonAction]
        public ActionResult RenderError404()
        {
            //return RedirectToAction("Error404", "Error", new { aspxerrorpath = Request.Url.PathAndQuery });
            return Error404(
                Request.Url
                .ShowWhenNullOrEmpty(new Uri("http://localhost/"))
                .PathAndQuery);//不转向
        }
        /// <summary>
        /// 显示404页面
        /// </summary>
        /// <param name="aspxerrorpath"></param>
        /// <returns></returns>
        [NonAction]
        public virtual ActionResult Error404(string aspxerrorpath = null)
        {
            if (string.IsNullOrEmpty(aspxerrorpath) && Request.UrlReferrer != null)
            {
                aspxerrorpath = Request.UrlReferrer.PathAndQuery;
            }
            var vd = new HttpErrorViewModel
            {
                Url = aspxerrorpath,
                ErrCode = 404
            };
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var result = View("Error404", vd);//HttpNotFound();
            return result;
        }

        #endregion

        /// <summary>
        /// 显示Json结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="result">数据，如果失败，可以为字符串的说明</param>
        /// <returns></returns>
        [NonAction]
        public JsonResult RenderJsonSuccessResult(bool success, object result, JsonRequestBehavior jsonRequestBehavior)
        {
            return Json(new
            {
                Success = success,
                Result = result
            }, jsonRequestBehavior);
        }

        [NonAction]
        public JsonResult RenderJsonSuccessResult(bool success, object result, bool allowGet = false)
        {
            return RenderJsonSuccessResult(success, result, allowGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet);
        }

        public void SetMessager(MessageLevel messageType, string messageText, bool showClose = true)
        {
            if (TempData["Messager"] == null || !(TempData["Messager"] is List<Messager>))
            {
                TempData["Messager"] = new List<Messager>();
            }

            (TempData["Messager"] as List<Messager>).Add(new Messager(messageType, messageText, showClose));
        }
    }
}

