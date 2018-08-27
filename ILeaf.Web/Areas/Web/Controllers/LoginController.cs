using ILeaf.Core.Config;
using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Core.Utilities;
using ILeaf.Service;
using ILeaf.Web.Areas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {

        }

        // GET: Web/Login
        public ActionResult Index(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                MessagerList = new List<Messager>()
            };

            if (TempData["AuthorityNotReach"] != null && TempData["AuthorityNotReach"].ToString() == "TRUE")
            {
                model.MessagerList.Add(new Messager(MessageLevel.Error, "您无权访问此页面或执行此操作！"));
                model.AuthorityNotReach = true;
            }

            model.IsLogined = HttpContext.User.Identity.IsAuthenticated;

            var trirdTimes = 0;
            if (Session["LoginTriedTime"] != null)
                trirdTimes = (int)Session["LoginTriedTime"];

            model.ShowVerificationCode = trirdTimes > SiteConfig.TryUserLoginTimes;

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            string error = null;

            IAccountService accountService = null;
            Account account = null;
            if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
            {
                error = "提交参数不完整";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                    account = accountService.GetAccount(model.UserName);
                    if (account == null)
                    {
                        error = "用户名不存在";
                    }
                    else if (accountService.TryLogin(model.UserName, model.Password, true,true) == null)
                    {
                        error = "密码错误";
                    }
                }
            }

            if (!error.IsNullOrEmpty() || !ModelState.IsValid)
            {
                var tryLoginTimes = 0;
                if (Session["TryLoginTimes"] != null)
                {
                    tryLoginTimes = (int)Session["TryLoginTimes"];
                }

                model.ShowVerificationCode = tryLoginTimes >= SiteConfig.TryUserLoginTimes;

                Session["TryLoginTimes"] = tryLoginTimes + 1;

                model.MessagerList = new List<Messager>();
                model.MessagerList.Add(new Messager(MessageLevel.Error, error));
                return View(model);
            }
            Session["TryLoginTimes"] = null;//清空登录次数

            Logger.Account.InfoFormat("User login succeed：{0}", model.UserName);

            if (model.ReturnUrl.IsNullOrEmpty())
            {
                return RedirectToAction("Index", "Calendar");
            }
            else
            {
                return Redirect(model.ReturnUrl.UrlDecode());
            }
        }
    }
    
}