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
    [ILeafAuthorize]
    [Menu("Calendar")]
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
            List<Account> friends = StructureMap.ObjectFactory.GetInstance<IFriendshipService>().GetAllFriend();
            List<Account> classmates = StructureMap.ObjectFactory.GetInstance<IAccountService>()
                .GetObjectList(0, 0, x => x.SchoolId == Account.SchoolId && x.ClassId == Account.ClassId, x => x.Id, Core.Enums.OrderingType.Ascending);
            List<Group> groups = StructureMap.ObjectFactory.GetInstance<IGroupService>().GetGroups();


            return View(new AddAppointmentViewModel()
            {
                Json = json.IsNullOrEmpty() ? "{}" : json,
                Account = Account,
                MessagerList = new List<Messager>(),
                Friends = friends,
                Classmates = classmates,
                Groups = groups,
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
                    if (appointment.Groups.Count == 0)
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
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUnconfirmedFriendEvents()
        {
            try
            {
                var appointments = service.GetAllAppointmentInvition(Account.Id).ConvertAll(x => x.Appointment);
                List<object> list = new List<object>();
                foreach (Appointment appointment in appointments)
                {
                    if (appointment.Groups.Count == 0)
                    {
                        list.Add(new
                        {
                            id = "unconfirmed-" + appointment.Id.ToString(),
                            title = appointment.Title,
                            detail = appointment.Details,
                            allDay = appointment.IsAllDay,
                            start = appointment.StartTime.ToString(),
                            end = appointment.EndTime.ToString(),
                            place = appointment.Place,
                            editable = false,
                            userName = appointment.Creater.RealName,
                            user = appointment.CreatorId,
                            visiblity = appointment.Visibily,
                        });
                    }
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUserDisplayEvents(string userId)
        {
            try
            {
                List<Appointment> appointments = service.ShowAppointmentsToCurrentUser(Int64.Parse(userId));
                List<object> list = new List<object>();
                foreach (Appointment appointment in appointments)
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
                        editable = false,
                        user = appointment.CreatorId,
                        visiblity = appointment.Visibily,
                    });
                }

                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFriendAndGroupEvents()
        {
            try
            {
                IEnumerable<Appointment> appointments = service.GetFriendAppointment(Account.Id);

                foreach(var gm in Account.BelongToGroups)
                {
                    var groupid = gm.GroupId;
                    appointments = appointments.Union(service.GetGroupAppointments(groupid));
                }

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
                        editable = appointment.CreatorId == Account.Id,
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

        public ActionResult GetGroupAppointments(string groupId)
        {
            try
            {
                List<Appointment> appointments = service.GetGroupAppointments(Int64.Parse(groupId));
                List<object> list = new List<object>();
                foreach (Appointment appointment in appointments)
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
                        editable = appointment.CreatorId == Account.Id,
                        user = appointment.CreatorId,
                        visiblity = appointment.Visibily,
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

                    if (appointment.IsAllDay)
                    {
                        var start = model.StartTime;
                        var date = model.StartDate;
                        model.EndDate = date;
                        model.EndTime = TimeSpan.Parse(start).Add(TimeSpan.Parse("02:00")).ToString();
                    }

                    if (!model.Details.IsNullOrEmpty())
                        appointment.Details = model.Details;

                    appointment.IsAllDay = model.IsAllDay;

                    if (model.EndDate == "Invalid date")
                        model.EndDate = model.StartDate;

                    if (!model.IsAllDay)
                    {
                        appointment.StartTime = DateTime.Parse(model.StartDate).Add(TimeSpan.Parse(model.StartTime));
                        appointment.EndTime = (DateTime?)(DateTime.Parse(model.EndDate)).Add(TimeSpan.Parse(model.EndTime));
                    }
                    else
                    {
                        appointment.StartTime = DateTime.Parse(model.StartDate);
                        appointment.EndTime = DateTime.Parse(model.EndDate);
                    }

                    appointment.Visibily = byte.Parse(model.Visiblity);

                    if (!model.Place.IsNullOrEmpty())
                        appointment.Place = model.Place;

                    service.SaveObject(appointment);
                }
                else
                {
                    appointment = new Appointment()
                    {
                        Title = model.Title,
                        StartTime = model.IsAllDay ? DateTime.Parse(model.StartDate) : DateTime.Parse(model.StartDate).Add(TimeSpan.Parse(model.StartTime)),
                        EndTime = model.IsAllDay ? DateTime.Parse(model.EndDate) : DateTime.Parse(model.EndDate).Add(TimeSpan.Parse(model.EndTime)),
                        Details = model.Details,
                        IsAllDay = model.IsAllDay,
                        CreationTime = DateTime.Now,
                        CreatorId = Account.Id,
                        Place = model.Place,
                        Visibily = 0,
                    };

                    if (!model.ShareInfo.IsNullOrEmpty() && model.ShareInfo != "|")
                    {
                        foreach (string str in model.ShareInfo.Split('|'))
                        {
                            if (str.StartsWith("g-"))
                            {
                                var str2 = str.Substring(2);
                                service.SendAppointmentToGroup(appointment, Int64.Parse(str2));
                            }
                            else if (str.IsNullOrEmpty()) ;
                            else
                            {
                                service.SendAppointmentInvition(appointment, Int64.Parse(str));
                            }
                        }
                    }
                    else
                    {
                        service.SaveObject(appointment);
                    }
                }
                
                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}