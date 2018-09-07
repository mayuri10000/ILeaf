using ILeaf.Core.Extensions;
using ILeaf.Core.Models;
using ILeaf.Repository;
using ILeaf.Service;
using ILeaf.Web.Areas.Web.Models;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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

        public ActionResult GetCourses()
        {
            try
            {
                List<Course> courses = service.GetAllCourseForUser(Account.Id);
                List<object> list = new List<object>();
                foreach(Course course in courses)
                {
                    list.Add(new
                    {
                        id = "c" + course.Id.ToString(),
                        title = course.Title,
                        startHour = course.StartTime.Hours,
                        startMinute = course.StartTime.Minutes,
                        endHour = course.EndTime.Hours,
                        endMinutes = course.EndTime.Minutes,
                        weeks = Array.ConvertAll(course.Weeks, x => (int)x),
                        weekday = course.Weekday,
                        teacher = course.TeacherId,
                        classroom = course.Classroom
                    });
                }

                return Json(new { errCode = 0, courses = list });
            }
            catch(Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message });
            }
        }

        public ActionResult GetCourseChanges()
        {
            try
            {
                ICourseChangeRepository repository = StructureMap.ObjectFactory.GetInstance<ICourseChangeRepository>();
                List<CourseChange> changes = repository.GetObjectList(x => x.Course.Classes.Contains(Account.Class),
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

                return Json(new { errCode = 0, changes = list });
            }
            catch (Exception e)
            {
                return Json(new { errCode = -1, msg = e.Message });
            }
        }
    }
}