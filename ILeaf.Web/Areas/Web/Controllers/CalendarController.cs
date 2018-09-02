using ILeaf.Core.Enums;
using ILeaf.Core.Extensions;
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
            return View(new AddAppointmentViewModel()
            {
                Account = Account,
                MessagerList = new List<Messager>()
            });
        }

        public ActionResult AddAppointment(string json)
        {
            ViewBag.json = json.IsNullOrEmpty() ? "{}" : json;
            return View(new AddAppointmentViewModel()
            {
                Account = Account,
                MessagerList = new List<Messager>()
            });
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
                        detail = appointment.Details,
                        allDay = appointment.IsAllDay,
                        start = appointment.StartTime.ToString(),
                        end = appointment.EndTime.ToString(),
                        place = appointment.Place,
                        editable = true,
                        user = appointment.CreatorId,
                        visiblity = appointment.Visibily,
                    });
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
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
                        detail = appointment.Details,
                        allDay = appointment.IsAllDay,
                        place = appointment.Place,
                        start = appointment.StartTime.ToString(),
                        end = appointment.EndTime.ToString(),
                        editable = true,
                        user = appointment.CreatorId,
                        visiblity = appointment.Visibily
                    });
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateAppointment(AddAppointmentViewModel model)
        {
            try
            {
                IAccountService account_service = StructureMap.ObjectFactory.GetInstance<IAccountService>();
                IGroupService group_service = StructureMap.ObjectFactory.GetInstance<IGroupService>();
                Appointment appointment = null;

                if (!model.Id.IsNullOrEmpty())
                {
                    long id = Int64.Parse(model.Id);

                    appointment = service.GetObject(id);

                    if (!model.Details.IsNullOrEmpty())
                        appointment.Details = model.Details;

                    appointment.IsAllDay = model.IsAllDay;

                    if (!model.IsAllDay)
                    {
                        appointment.StartTime = DateTime.Parse(model.StartDate).Add(TimeSpan.Parse(model.StartTime));
                        appointment.EndTime = (DateTime?)(DateTime.Parse(model.EndDate)).Add(TimeSpan.Parse(model.EndTime));
                    }
                    else
                    {
                        appointment.StartTime = DateTime.Parse(model.StartDate);
                    }

                    appointment.Visibily = byte.Parse(model.Visiblity);

                    if (!model.Place.IsNullOrEmpty())
                        appointment.Place = model.Place;
                }
                else
                {
                    appointment = new Appointment()
                    {
                        Title = model.Title,
                        StartTime = DateTime.Parse(model.StartDate).Add(TimeSpan.Parse(model.StartTime)),
                        EndTime = model.IsAllDay ? null : (DateTime?)(DateTime.Parse(model.EndDate)).Add(TimeSpan.Parse(model.EndTime)),
                        Details = model.Details,
                        IsAllDay = model.IsAllDay,
                        CreationTime = DateTime.Now,
                        CreatorId = Account.Id,
                        Place = model.Place,
                        Visibily = 0,
                    };
                }
                service.SaveObject(appointment);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}