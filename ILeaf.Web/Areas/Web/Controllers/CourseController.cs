using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Repository;
using ILeaf.Service;
using ILeaf.Web.Areas.Web.Models;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using ILeaf.Web.Utils;
using StructureMap;

namespace ILeaf.Web.Areas.Web.Controllers
{
    [Menu("Course")]
    [ILeafAuthorize]
    public class CourseController : BaseController
    {
        ICourseService service = StructureMap.ObjectFactory.GetInstance<ICourseService>();
        // GET: Web/Course
        public ActionResult Index(string courseId)
        {
            Course course = courseId.IsNullOrEmpty() ? null : service.GetObject(Int64.Parse(courseId));
            List<Course> courses = service.GetAllCourseForUser(Account.Id);
            bool isTeacherOfThis = course == null ? false : course.TeacherId == Account.Id;          // TODO: Teachers only
            ICollection<AttachmentCourse> attachments = course == null ? null : course.Attachments;
            ICollection<CourseChange> courseChanges = course == null ? null : course.CourseChanges;

            return View(new CourseViewModel()
            {
                Courses = courses,
                CurrentCourse = course,
                IsTeacherOfThis = isTeacherOfThis,
                MessagerList = new List<Messager>(),
                Attachments = attachments,
                Changes = courseChanges
            });
        }

        public ActionResult Add(string courseId)
        {
            Course course = courseId.IsNullOrEmpty() ? null : service.GetObject(Int64.Parse(courseId));

            return View(new AddCourseViewModel()
            {
                Account = Account,
                MessagerList = new List<Messager>(),
                Course = course
            });
        }

        [HttpPost]
        public ActionResult Add()
        {
            if (Account.UserType != 3)
                return Content("您没有执行本操作的权限");
            try
            {
                if (Request.Form["Id"].IsNullOrEmpty())
                {
                    Course course = new Course()
                    {
                        SchoolId = Account.SchoolId.Value,
                        TeacherId = Account.Id,
                        Title = Request.Form["Title"],
                        SemesterStart = DateTime.Parse(Request.Form["SemesterStart"]),
                        Weeks = (from week in Request.Form["Weeks"].Split(',') select Byte.Parse(week)).ToArray(),
                        IsSelectableCourse = Boolean.Parse(Request.Form["IsSelective"]),
                    };

                    service.SaveObject(course);

                    ICourseClassRepository courseClassRepository = StructureMap.ObjectFactory.GetInstance<ICourseClassRepository>();
                    IClassInfoService classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
                    foreach (string clazz in Request.Form["Classes"].Split(','))
                    {
                        if (!clazz.IsNullOrEmpty())
                        {
                            var clas = classInfoService.GetObject(x => x.SchoolId == Account.SchoolId && x.ClassName == clazz);
                            courseClassRepository.Add(new CourseClass()
                            {
                                Classes_Id = clas.Id,
                                Courses_Id = course.Id
                            });
                        }
                    }

                    courseClassRepository.SaveChanges();

                    ICourseTimeRepository courseTimeRepository = StructureMap.ObjectFactory.GetInstance<ICourseTimeRepository>();

                    int i = 1;
                    while (true)
                    {
                        if (!Request.Form.AllKeys.Contains("Weekday_" + i))
                            break;

                        var weekday = Request.Form["Weekday_" + i];
                        var start = Request.Form["StartTime_" + i];
                        var end = Request.Form["EndTime_" + i];
                        var classroom = Request.Form["Classroom_" + i];

                        courseTimeRepository.Save(new CourseTime()
                        {
                            CourseId = course.Id,
                            Classroom = classroom,
                            Weekday = Byte.Parse(weekday),
                            EndTime = TimeSpan.Parse(end),
                            StartTime = TimeSpan.Parse(start)
                        });

                        i++;
                    }

                    courseTimeRepository.SaveChanges();

                    return Content("success");
                }
                else
                {
                    Course course = service.GetObject(Int64.Parse(Request.Form["Id"]));

                    course.Classes.Clear();
                    ICourseClassRepository courseClassRepository = StructureMap.ObjectFactory.GetInstance<ICourseClassRepository>();
                    IClassInfoService classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
                    foreach (string clazz in Request.Form["Classes"].Split(','))
                    {
                        if (!clazz.IsNullOrEmpty())
                        {
                            var clas = classInfoService.GetObject(x => x.SchoolId == Account.SchoolId && x.ClassName == clazz);
                            courseClassRepository.Add(new CourseClass()
                            {
                                Classes_Id = clas.Id,
                                Courses_Id = course.Id
                            });
                        }
                    }

                    courseClassRepository.SaveChanges();

                    course.Title = Request.Form["Title"];
                    //course.StartTime = TimeSpan.Parse(model.StartTime);
                    //course.EndTime = TimeSpan.Parse(model.EndTime);
                    course.SemesterStart = DateTime.Parse(Request.Form["SemesterStart"]);
                    //course.Weekday = Byte.Parse(model.Weekday);
                    course.Weeks = (from week in Request.Form["Weeks"].Split(',') select Byte.Parse(week)).ToArray();
                    course.IsSelectableCourse = Boolean.Parse(Request.Form["IsSelective"]);

                    service.SaveObject(course);

                    course.CourseTimes.Clear();

                    int i = 1;
                    while (true)
                    {
                        if (Request.Form["Weekday_" + i].IsNullOrEmpty())
                            break;

                        var weekday = Request.Form["Weekday_" + i];
                        var start = Request.Form["StartTime_" + i];
                        var end = Request.Form["EndTime_" + i];
                        var classroom = Request.Form["Classroom_" + i];

                        course.CourseTimes.Add(new CourseTime()
                        {
                            CourseId = course.Id,
                            Classroom = classroom,
                            Weekday = Byte.Parse(weekday),
                            EndTime = TimeSpan.Parse(end),
                            StartTime = TimeSpan.Parse(start)
                        });
                    }

                    service.SaveObject(course);

                    return Content("success");
                }
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
        
        public ActionResult AddCourseChange(AddCourseChangeModel model)
        {
            try
            {
                if (Account.UserType != 3)
                    return Content("您没有执行本操作的权限");

                if (!model.Date.IsNullOrEmpty() && !model.StartTime.IsNullOrEmpty() && !model.EndTime.IsNullOrEmpty())
                {
                    DateTime date = DateTime.Parse(model.Date);
                    DateTime start = date.Add(TimeSpan.Parse(model.StartTime));
                    DateTime end = date.Add(TimeSpan.Parse(model.EndTime));

                    model.ChangeValue = String.Format("{0}-{1}", start.ToString(), end.ToString());
                }

                ICourseChangeRepository courseChangeRepository = StructureMap.ObjectFactory.GetInstance<ICourseChangeRepository>();
                courseChangeRepository.Add(new CourseChange()
                {
                    CourseId = model.CourseId,
                    CourseTime = DateTime.Parse(model.CourseTime),
                    ChangeType = (byte)((int)Enum.Parse(typeof(Core.Enums.CourseChangeType), model.ChangeType)),
                    ChangedValue = model.ChangeValue
                });

                courseChangeRepository.SaveChanges();

                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteCourse(string courseId)
        {
            try
            {
                Course c = service.GetObject(Int64.Parse(courseId));

                ICourseChangeRepository courseChangeRepository = ObjectFactory.GetInstance<ICourseChangeRepository>();
                ICourseClassRepository courseClassRepository = ObjectFactory.GetInstance<ICourseClassRepository>();
                ICourseTimeRepository courseTimeRepository = ObjectFactory.GetInstance<ICourseTimeRepository>();
                IAttachmentCourseRepository attachmentCourseRepository = ObjectFactory.GetInstance<IAttachmentCourseRepository>();
                ISelectableCourseStudentRepository selectableCourseStudentRepository = ObjectFactory.GetInstance<ISelectableCourseStudentRepository>();

                foreach (var x in attachmentCourseRepository.GetObjectList(x => x.CourseId == c.Id, x => x.CourseId, Core.Enums.OrderingType.Ascending, 0, 0))
                    attachmentCourseRepository.Delete(x);

                foreach (var x in courseClassRepository.GetObjectList(x => x.Courses_Id == c.Id, x => x.Courses_Id, Core.Enums.OrderingType.Ascending, 0, 0))
                    courseClassRepository.Delete(x);

                foreach (var x in courseChangeRepository.GetObjectList(x => x.CourseId == c.Id, x => x.CourseId, Core.Enums.OrderingType.Ascending, 0, 0))
                    courseChangeRepository.Delete(x);

                foreach (var x in courseTimeRepository.GetObjectList(x => x.CourseId == c.Id, x => x.CourseId, Core.Enums.OrderingType.Ascending, 0, 0))
                    courseTimeRepository.Delete(x);

                foreach (var x in selectableCourseStudentRepository.GetObjectList(x => x.SelectedCourses_Id == c.Id, x => x.SelectedCourses_Id, Core.Enums.OrderingType.Ascending, 0, 0))
                    selectableCourseStudentRepository.Delete(x);

                service.DeleteObject(c);
                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteCourseChange()
        {
            try
            {
                var cid = Int64.Parse(Request.QueryString["cid"]);
                var time = DateTime.Parse(Request.QueryString["time"]);

                ICourseChangeRepository courseChangeRepository = StructureMap.ObjectFactory.GetInstance<ICourseChangeRepository>();

                courseChangeRepository.Delete(courseChangeRepository.GetFirstOrDefaultObject(x => x.CourseTime == time && x.CourseId == cid));

                return Content("success");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult DeleteAttachment()
        {
            try
            {
                var aid = Int64.Parse(Request.QueryString["aid"]);
                var cid = Int64.Parse(Request.QueryString["cid"]);

                IAttachmentCourseRepository attachmentCourseRepository = ObjectFactory.GetInstance<IAttachmentCourseRepository>();
                IAttachmentService attachmentService = ObjectFactory.GetInstance<IAttachmentService>();

                attachmentCourseRepository.Delete(attachmentCourseRepository.GetFirstOrDefaultObject(x => x.AttachmentId == aid && x.CourseId == cid));

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public ActionResult GetCourses()
        {
            try
            {
                List<Course> courses = service.GetAllCourseForUser(Account.Id);
                List<object> list = new List<object>();
                foreach(Course course in courses)
                {
                    foreach (CourseTime t in course.CourseTimes)
                    {
                        list.Add(new
                        {
                            id = course.Id,
                            title = course.Title,
                            startHour = t.StartTime.Hours,
                            startMinute = t.StartTime.Minutes,
                            endHour = t.EndTime.Hours,
                            semester = course.SemesterStart,
                            endMinutes = t.EndTime.Minutes,
                            weeks = Array.ConvertAll(CourseUtils.TrimWeeks(course.Weeks), x => (int)x),
                            weekday = t.Weekday,
                            teacher = course.TeacherId,
                            classroom = t.Classroom
                        });
                    }
                }

                return Json(new { errCode = 0, courses = list }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult VerifyClass(string name)
        {
            IClassInfoService classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
            bool vaild = classInfoService.GetObject(x => x.SchoolId == Account.SchoolId && x.ClassName == name) != null;
            return Content(vaild ? "success" : "failed");
        }

        public ActionResult GetCourseChanges()
        {
            try
            {
                ICourseChangeRepository repository = StructureMap.ObjectFactory.GetInstance<ICourseChangeRepository>();
                List<CourseChange> changes = null;
                if (Account.UserType == 2)
                    changes = repository.GetObjectList(
                    m => m.Course.Classes.Where(x => x.Classes_Id == Account.ClassId).FirstOrDefault() != null,
                    x => x.CourseId, Core.Enums.OrderingType.Ascending, 0, 0);
                else
                    changes = repository.GetObjectList(
                    m => Account.Courses.Where(x => x.Id == m.CourseId) != null,
                    x => x.CourseId, Core.Enums.OrderingType.Ascending, 0, 0);
                List<object> list = new List<object>();
                foreach (CourseChange change in changes)
                {
                    list.Add(new
                    {
                        courseId = change.CourseId,
                        type = ((Core.Enums.CourseChangeType)change.ChangeType).ToString(),
                        date = change.CourseTime,
                        changedValue = change.ChangedValue,
                    });
                }

                return Json(new { errCode = 0, changes = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}