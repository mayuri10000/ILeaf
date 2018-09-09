using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Service;
using ILeaf.Web.Areas.Web.Models;
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
    [ILeafAuthorize]
    [Menu("Contact")]
    public class AccountController : BaseController
    {
        IAccountService accountService = StructureMap.ObjectFactory.GetInstance<IAccountService>();
        IFriendshipService friendshipService = StructureMap.ObjectFactory.GetInstance<IFriendshipService>();

        public ActionResult Contact(string accountId)
        {
            List<Account> friends = friendshipService.GetAllFriend();
            List<Account> pendingFriends = friendshipService.GetPendingFriendRequests();
            List<Account> classmates = accountService.GetObjectList(0, 0, x => x.SchoolId == Account.SchoolId && x.ClassId == Account.ClassId, x => x.Id, Core.Enums.OrderingType.Ascending);
            Account currentAccount = accountId.IsNullOrEmpty() ? null : accountService.GetObject(Int64.Parse(accountId));
            List<Group> groups = currentAccount == null ? null : StructureMap.ObjectFactory.GetInstance<IGroupService>().GetGroups(currentAccount.Id);
            bool isClassmate = currentAccount == null ? false : currentAccount.SchoolId == Account.SchoolId && currentAccount.ClassId == Account.ClassId;
            bool isSelf = currentAccount == null ? false : currentAccount.Id == Account.Id;
            bool isFriend = currentAccount == null ? false : friends.Where(x => x.Id == currentAccount.Id).FirstOrDefault() != null;
            bool isPendingFriend = currentAccount == null ? false : friendshipService.GetSentButNotAcceptedFriendRequests()
                .Where(x => x.Id == currentAccount.Id).FirstOrDefault() != null;

            return View(new ContactViewModel()
            {
                Account = Account,
                MessagerList = new List<Messager>(),
                Friends = friends,
                PendingFriends = pendingFriends,
                Classmates = classmates,
                CurrentAccount = currentAccount,
                IsClassmate = isClassmate,
                IsSelf = isSelf,
                IsFriend = isFriend,
                IsPendingFriend = isPendingFriend,
                Groups = groups
            });
        }

        public ActionResult SearchUser(string keyword)
        {
            List<Account> users = accountService.GetObjectList(0, 0, u => u.RealName.Contains(keyword) ||
                u.UserName.Contains(keyword), u => u.Id, Core.Enums.OrderingType.Ascending);

            List<object> list = new List<object>();

            foreach(Account user in users)
            {
                list.Add(new
                {
                    id = user.Id,
                    realName = user.RealName,
                    userName = user.UserName,
                    img = user.HeadImgUrl,
                    school = user.School.SchoolName,
                    @class = user.UserType == 2 ? user.Class.ClassName : ""
                });
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendFriendRequest(string userId)
        {
            try
            {
                friendshipService.SendFriendRequestTo(Int64.Parse(userId));
                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult AcceptFriendRequest(string userId)
        {
            try
            {
                friendshipService.AcceptFriendshipRequestFrom(Int64.Parse(userId));
                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeclineFriendRequestOrRemoveFriend(string userId)
        {
            try
            {
                friendshipService.DeclineFriendshipRequestFromOrRemoveFriend(Int64.Parse(userId));
                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult Notifications()
        {
            var notifications = new List<Notification>()
            {
                new Notification(){Title="本页面仅供展示",Level=0,Text="本项目尚未完全完成"},
                new Notification(){Title="紧急通知",Level=1,Text="通知内容"},
                new Notification(){Title="普通通知",Level=0,Text="通知内容"},
                new Notification(){Title="普通通知",Level=0,Text="通知内容"},
            };

            return View(new NotificationsViewModel() {
                Account = Account,
                Notifications = notifications
            });
        }

        public ActionResult Logout()
        {
            accountService.Logout();
            return RedirectToAction("Index", "Login");
        }
    }
}