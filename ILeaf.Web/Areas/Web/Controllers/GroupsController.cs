using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Repository;
using ILeaf.Service;
using ILeaf.Web.Areas.Web.Models;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    [ILeafAuthorize]
    [Menu("Groups")]
    public class GroupsController : BaseController
    {
        IGroupService groupService = StructureMap.ObjectFactory.GetInstance<IGroupService>();
        // GET: Web/Groups
        public ActionResult Index(string groupId)
        {

            Group group = groupId.IsNullOrEmpty() ? null : groupService.GetObject(Int64.Parse(groupId));
            bool isGroupMember = group == null ? false : groupService.GetGroups().Where(g => g.Id == Int64.Parse(groupId)).FirstOrDefault() != null;
            bool isHeadman = group == null ? false : group.HeadmanId == Account.Id;
            bool isPendingMember = group == null ? false : groupService.GetPendingRequests(Int64.Parse(groupId)).Where(x => x.Id == Account.Id).FirstOrDefault() != null;

            var model = new GroupViewModel()
            {
                Groups = groupService.GetGroups(),
                CurrentGroup = group,
                IsGroupMember = isGroupMember,
                IsHeadman = isHeadman,
                IsPendingMember = isPendingMember,
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateGroup(string name)
        {
            try
            {
                Group group = new Group()
                {
                    Name = name,
                    CreationTime = DateTime.Now,
                    Announcement = "",
                    HeadmanId = Account.Id
                };
                groupService.SaveObject(group);
                groupService.AddMember(group.Id, Account.Id);
                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult EditAnnouncement()
        {
            try
            {
                var groupId = Int64.Parse(Request.Form["groupId"]);
                var announcement = Request.Form["announcement"];

                var group = groupService.GetObject(groupId);
                group.Announcement = announcement;
                groupService.SaveObject(group);

                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult EditName()
        {
            try
            {
                var groupId = Int64.Parse(Request.QueryString["groupId"]);
                var name = Request.QueryString["name"];

                var group = groupService.GetObject(groupId);
                group.Name = name;
                groupService.SaveObject(group);

                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult ChangeHeadman()
        {
            try
            {
                var groupId = Int64.Parse(Request.QueryString["groupId"]);
                var uid = Int64.Parse(Request.QueryString["uid"]);

                groupService.ChangeHeadman(groupId, uid);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult LeaveGroup(string groupId)
        {
            try
            {
                groupService.DeleteMember(Int64.Parse(groupId), Account.Id);
                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult JoinGroup(string groupId)
        {
            try
            {
                groupService.SendGroupJoinRequest(Int64.Parse(groupId), Account.Id);
                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult RemoveMember()
        {
            try
            {
                var groupId = Int64.Parse(Request.QueryString["groupId"]);
                var uid = Int64.Parse(Request.QueryString["uid"]);

                groupService.DeleteMember(groupId, uid);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult AddMember()
        {
            try
            {
                var groupId = Int64.Parse(Request.QueryString["groupId"]);
                var uid = Int64.Parse(Request.QueryString["uid"]);

                groupService.AddMember(groupId, uid);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult ConfirmMember()
        {
            try
            {
                var groupId = Int64.Parse(Request.QueryString["groupId"]);
                var uid = Int64.Parse(Request.QueryString["uid"]);

                groupService.AcceptGroupJoinRequest(groupId, uid);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public ActionResult DissolveGroup(string groupId)
        {
            try
            {
                groupService.DeleteGroup(Int64.Parse(groupId));
                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}