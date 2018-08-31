using ILeaf.Core.Enums;
using ILeaf.Core.Models;
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
    [Auth]
    public class CalendarController : BaseController
    {
        IAppointmentService service = StructureMap.ObjectFactory.GetInstance<IAppointmentService>();

        // GET: Web/Calendar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPersonalEvents()
        {
            try
            {
                List<Appointment> appointments = service.GetPersonalAppointments(Account.Id);
                List<object> list = new List<object>();
                foreach(Appointment appointment in appointments)
                {
                    list.Add(new
                    {
                        id = appointment.Id.ToString(),
                        title = appointment.Title,
                        allDay = appointment.IsAllDay,
                        start = appointment.StartTime.ToString(),
                        end = appointment.EndTime.ToString(),
                        editable = true,
                        user = appointment.CreatorId
                    });
                }

                return Json(list);
            }
            catch(Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message });
            }
        }

        public ActionResult GetFriendAndGroupEvents()
        {
            try
            {
                var appointments = service.GetGroupAppointments(Account.Id).Union(service.GetFriendAppointment(Account.Id));
                List<object> list = new List<object>();
                foreach (Appointment appointment in appointments)
                {
                    list.Add(new
                    {
                        id = appointment.Id.ToString(),
                        title = appointment.Title,
                        allDay = appointment.IsAllDay,
                        start = appointment.StartTime.ToString(),
                        end = appointment.EndTime.ToString(),
                        editable = true,
                        user = appointment.CreatorId
                    });
                }

                return Json(list);
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message });
            }
        }

        [HttpPost]
        public ActionResult AddOrModifyEvent(AddOrModifyAppointmentViewModel model)
        {
            try
            {
                IAccountService account_service = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                IGroupService group_service = StructureMap.ObjectFactory.GetInstance<IGroupService>();

                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    StartTime = DateTime.Parse(model.StartTime),
                    EndTime = model.IsAllDay ? null : (DateTime?)(DateTime.Parse(model.EndTime)),
                    Details = model.Details,
                    IsAllDay = model.IsAllDay,
                    CreatorId = Account.Id,
                    Place = model.Place,
                    Visibily = (byte)(Enum.Parse(typeof(AppointmentVisiblity), model.Visiblity))
                };

                service.SaveObject(appointment);

                foreach (string s in model.ShareUsersAndGroups.Split(','))
                {
                    if (s.StartsWith("g:"))
                    {
                        long gid = Int64.Parse(s.Substring(2));
                        service.SendAppointmentToGroup(appointment.Id, gid);
                    }
                    else
                    {
                        long uid = Int64.Parse(s);
                        service.SendAppointmentInvition(appointment.Id, uid);
                    }
                }

                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}