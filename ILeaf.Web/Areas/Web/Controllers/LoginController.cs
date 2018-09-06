using ILeaf.Core.Config;
using ILeaf.Core.Enums;
using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Core.Utilities;
using ILeaf.Core.Utility;
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
            else
                Session["LoginTriedTime"] = 0;

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
                    if ((int)Session["LoginTriedTime"] >= SiteConfig.TryUserLoginTimes)
                    {
                        string code = (string)TempData["VerificationCode"] as string;
                        if (model.VerificationCode.IsNullOrEmpty())
                        {
                            error = "请输入验证码";
                        }
                        else if (!model.VerificationCode.Equals(code, StringComparison.OrdinalIgnoreCase))
                        {
                            error = "验证码不正确";
                        }
                    }
                    else if (account == null)
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
                if (Session["LoginTriedTime"] != null)
                {
                    tryLoginTimes = (int)Session["LoginTriedTime"];
                }

                model.ShowVerificationCode = tryLoginTimes >= SiteConfig.TryUserLoginTimes;

                Session["LoginTriedTime"] = tryLoginTimes + 1;

                model.MessagerList = new List<Messager>();
                model.MessagerList.Add(new Messager(MessageLevel.Error, error));
                return View(model);
            }
            Session["LoginTriedTime"] = 0;//清空登录次数

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

        public ActionResult VerificationCode()
        {
            var code = new ValidationCode();
            var text = code.GetRandomString(4);
            TempData["VerificationCode"] = text;
            var image = code.CreateImage(text);
            return File(image, "image/bmp");
        }

        public ActionResult ForgetPassword()
        {
            return View(new ForgetPasswordViewModel() {
                MessagerList = new List<Messager>()
            });
        }

        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            string error = null;
            if (model.VerificationCode.IsNullOrEmpty())
            {
                error = "请输入验证码";
            }
            else if (!model.VerificationCode.Equals((string)TempData["VerificationCode"], StringComparison.OrdinalIgnoreCase))
            {
                error = "验证码不正确";
            }
            else
            {

                IAccountService accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                Account account = accountService.GetAccount(model.EMail);

                if (account == null)
                {
                    error = "该邮箱尚未注册";
                }
                else
                {
                    // TODO: 发送邮件
                }
            }
            if (!error.IsNullOrEmpty() || !ModelState.IsValid)
            {
                model.MessagerList = new List<Messager>();
                model.MessagerList.Add(new Messager(MessageLevel.Error, error));
                return View(model);
            }

            Logger.Account.InfoFormat("Password retrive request：{0}", model.EMail);

            model.MessagerList = new List<Messager>();
            model.MessagerList.Add(new Messager(MessageLevel.Success, "系统已向您的邮箱发送了一封验证邮件，请查收后按照指示修改密码"));
            return View(model);
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel()
            {
                MessagerList = new List<Messager>()
            });
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            string error = null;
            if (model.VerificationCode.IsNullOrEmpty())
            {
                error = "请输入验证码";
            }
            else if (!model.VerificationCode.Equals((string)TempData["VerificationCode"], StringComparison.OrdinalIgnoreCase))
            {
                error = "验证码不正确";
            }
            else
            {
                var schoolInfoService = StructureMap.ObjectFactory.GetInstance<ISchoolInfoService>();
                var classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
                var accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();

                if (accountService.GetObject(x => x.UserName == model.UserName) != null)
                {
                    error = "用户名已存在";
                    goto er;
                }
                else if (accountService.GetObject(x => x.Email == model.EMail) != null)
                {
                    error = "邮箱地址已被注册";
                    goto er;
                }

                var school = schoolInfoService.GetObject(s => s.SchoolName == model.SchoolName);
                if (school==null)
                {
                    error = "学校名称不存在";
                    goto er;
                }

                var clazz = classInfoService.GetObject(c => c.SchoolId == school.SchoolId && c.ClassName == model.ClassName);
                if (clazz == null)
                {
                    error = "班级名称不存在";
                    goto er;
                }

                accountService.Register(model.UserName,
                    model.EMail,
                    model.Password,
                    (Gender)Int32.Parse(model.Gender),
                    (UserType)Int32.Parse(model.UserType),
                    school.SchoolId,
                    clazz.Id,
                    model.SchoolCardNum,
                    model.RealName);
            }

er:         if (!error.IsNullOrEmpty() || !ModelState.IsValid)
            {
                model.MessagerList = new List<Messager>();
                model.MessagerList.Add(new Messager(MessageLevel.Error, error));
                return View(model);
            }

            Logger.Account.InfoFormat("User Registered：{0}", model.UserName);

            model.MessagerList = new List<Messager>();
            model.MessagerList.Add(new Messager(MessageLevel.Success, "注册成功，请前往登录界面登录"));
            return View(model);
        }

        [HttpGet]
        public ActionResult CheckIfUserNameOrEmailExist(string userName)
        {
            if (!userName.IsNullOrEmpty())
            {
                IAccountService accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                if (accountService.GetAccount(userName) != null)
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckSchoolName(string schoolName)
        {
            if (!schoolName.IsNullOrEmpty())
            {
                ISchoolInfoService schoolInfoService = StructureMap.ObjectFactory.GetInstance<ISchoolInfoService>();
                if (schoolInfoService.GetObject(x => x.SchoolName == schoolName) == null)
                    return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchoolAutoComplete()
        {
            var term = Request.QueryString["term"];
            var schoolInfoService = StructureMap.ObjectFactory.GetInstance<ISchoolInfoService>();
            var result = schoolInfoService.GetObjectList(0, 0, x => x.SchoolName.StartsWith(term), x => x.SchoolId, Core.Enums.OrderingType.Ascending);
            var str = "";

            for(int i = 0; i != result.Count; i++)
            {
                str += result[i].SchoolName;
                if (i != result.Count - 1)
                    str += "|";
            }

            return Content(str);
        }
    }
    
}