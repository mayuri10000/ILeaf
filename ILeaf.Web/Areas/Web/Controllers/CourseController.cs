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
            ICollection<AttachmentCourse> attachments = course == null ? null : course.AttachmentCourses;
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
        public ActionResult Add(AddCourseViewModel model)
        {
            if (Account.UserType != 3)
                return Content("您没有执行本操作的权限");
            try
            {
                if (model.Id.IsNullOrEmpty())
                {
                    Course course = new Course()
                    {
                        SchoolId = Account.SchoolId.Value,
                        TeacherId = Account.Id,
                        Title = model.Title,
                        SemesterStart = DateTime.Parse(model.SemesterStart),
                        Weeks = (from week in model.Weeks.Split(',') select Byte.Parse(week)).ToArray(),
                        IsSelectableCourse = model.IsSelective,
                    };

                    service.SaveObject(course);

                    IClassInfoService classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
                    foreach (string clazz in model.Classes.Split(','))
                    {
                        if (!clazz.IsNullOrEmpty())
                            course.Classes.Add(classInfoService.GetClassByName(clazz, Account.SchoolId.Value));
                    }

                    return Content("success");
                }
                else
                {
                    Course course = service.GetObject(Int64.Parse(model.Id));

                    course.Classes.Clear();
                    IClassInfoService classInfoService = StructureMap.ObjectFactory.GetInstance<IClassInfoService>();
                    foreach (string clazz in model.Classes.Split(','))
                    {
                        if (!clazz.IsNullOrEmpty())
                            course.Classes.Add(classInfoService.GetClassByName(clazz, Account.SchoolId.Value));
                    }

                    course.Title = model.Title;
                    //course.StartTime = TimeSpan.Parse(model.StartTime);
                    //course.EndTime = TimeSpan.Parse(model.EndTime);
                    course.SemesterStart = DateTime.Parse(model.SemesterStart);
                    //course.Weekday = Byte.Parse(model.Weekday);
                    course.Weeks = (from week in model.Weeks.Split(',') select Byte.Parse(week)).ToArray();
                    course.IsSelectableCourse = model.IsSelective;

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

                Course course = service.GetObject(model.CourseId);
                course.CourseChanges.Add(new CourseChange()
                {
                    CourseId = model.CourseId,
                    CourseTime = DateTime.Parse(model.CourseTime),
                    ChangeType = (byte)Enum.Parse(typeof(Core.Enums.CourseChangeType), model.ChangeType),
                    ChangedValue = model.ChangeValue
                });

                return Content("success");
            }
            catch(Exception e)
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
                            id = "c" + course.Id.ToString(),
                            title = course.Title,
                            startHour = t.StartTime.Hours,
                            startMinute = t.StartTime.Minutes,
                            endHour = t.EndTime.Hours,
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

        public ActionResult GetCourseChanges()
        {
            try
            {
                ICourseChangeRepository repository = StructureMap.ObjectFactory.GetInstance<ICourseChangeRepository>();
                List<CourseChange> changes = repository.GetObjectList(m => m.Course.Classes.Where(x => x.Id == Account.ClassId).FirstOrDefault() != null,
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