using ILeaf.Core.Extensions;
using ILeaf.Service;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using ILeaf.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    [Auth]
    public class AccountController : BaseController
    {
        public ActionResult Contact(string accountId)
        {
            if(accountId.IsNullOrEmpty())
                ViewBag.Account = null;
            else
            {
                IAccountService accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                IFriendshipService friendshipService = StructureMap.ObjectFactory.GetInstance<IFriendshipService>();
                ViewBag.Account = accountService.GetObject(Int64.Parse(accountId));
                ViewBag.IsClassmate = Account.ClassId == ViewBag.Account.ClassId;
                //ViewBag.IsFriend = friendshipService.GetObject(x => x.IsAccepted && ((x.Account1 == Account.Id && x.Account2 == Int64.Parse(accountId)) ||
                //  (x.Account2 == Account.Id && x.Account1 == (long)ViewBag.Account.Id)));
            }
            return View(new BaseViewModel() {
                Account = Account,
                MessagerList = new List<Core.Models.Messager>(),
                CurrentMenu = "Contact",
            });
        }

        
    }
}