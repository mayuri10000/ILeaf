using ILeaf.Core.Models;
using ILeaf.Service;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    [Auth]
    public class GroupsController : BaseController
    {
        IGroupService groupService = StructureMap.ObjectFactory.GetInstance<IGroupService>();
        // GET: Web/Groups
        public ActionResult Index()
        {
            return View();
        }

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
    }
}