using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;
using ILeaf.Core;

namespace ILeaf.Service
{
    public interface ICourseService : IBaseService<Course>
    {
        void AddCourseChangeInfo(long courseId, DateTime time, string changedValue, CourseChangeType type, string description = "");
        List<Tuple<DateTime,DateTime>> GetAllTimeForCourse(long courseId);
        void AddAttachmentForCourse(long courseId, long attachmentId, DateTime time);
        List<Course> GetAllCourseForUser(long userId);
    }

    public class CourseService : BaseService<Course>, ICourseService
    {
        ICourseChangeRepository courseChangeRepository = ObjectFactory.GetInstance<ICourseChangeRepository>();

        public CourseService(ICourseRepository repo) : base(repo) { }

        public void AddAttachmentForCourse(long courseId, long attachmentId, DateTime time)
        {
            Account user = Server.HttpContext.Session["Account"] as Account;
            Course course = GetObject(courseId);
            IAttachmentCourseRepository x = StructureMap.ObjectFactory.GetInstance<IAttachmentCourseRepository>();
            x.Add(new AttachmentCourse()
            {
                AttachmentId = attachmentId,
                CourseId = courseId,
                CourseTime = time
            });

            x.SaveChanges();
        }

        public void AddCourseChangeInfo(long courseId, DateTime time, string changedValue, CourseChangeType type, string description = "")
        {
            CourseChange change = new CourseChange()
            {
                CourseId = courseId,
                CourseTime = time,
                ChangedValue = changedValue,
                ChangeType = (byte)type,
            };
            courseChangeRepository.Save(change);
        }

        public List<Course> GetAllCourseForUser(long userId)
        {
            IAccountService accountService = ObjectFactory.GetInstance<IAccountService>();
            Account account = accountService.GetObject(userId);
            if (account.UserType == (byte)UserType.Teacher)
                return GetFullList(c => c.TeacherId == userId && c.SemesterStart < DateTime.Now &&
                                   c.SemesterStart.AddDays(7 * c.Weeks.Max()) > DateTime.Now, c => c.Id, OrderingType.Ascending);
            else if (account.UserType == (byte)UserType.Student && account.ClassId != null)
            {
                var courses = GetFullList(c => c.ClassInfos.Contains(account.Class), c => c.Id, OrderingType.Ascending);
                courses.Union(account.ChosenCourses);
                return courses;
            }

            return new List<Course>();
        }

        public List<Tuple<DateTime, DateTime>> GetAllTimeForCourse(long courseId)
        {
            Course course = GetObject(courseId);
            List<Tuple<DateTime, DateTime>> ret = new List<Tuple<DateTime, DateTime>>();
            List<CourseChange> changes = courseChangeRepository.GetObjectList(c => c.CourseId == courseId, c => c.CourseTime, OrderingType.Ascending, 0, 0);
            DateTime start = course.SemesterStart;
            DateTime current = course.SemesterStart;
            byte weekIndex = 1;
            while (weekIndex <= 25)
            {
                if (current.DayOfWeek == (DayOfWeek)course.Weekday && course.Weeks.Contains(weekIndex))
                {
                    DateTime courseStart = current.AddHours(course.StartTime.Hours).AddMinutes(course.StartTime.Minutes);
                    DateTime courseEnd = current.AddHours(course.EndTime.Hours).AddMinutes(course.EndTime.Minutes);
                    ret.Add(new Tuple<DateTime, DateTime>(courseStart, courseEnd));
                }

                current.AddDays(1);
                if (current.DayOfWeek == DayOfWeek.Sunday)
                    weekIndex++;
            }

            foreach(CourseChange change in changes)
            {
                if ((CourseChangeType)change.ChangeType == CourseChangeType.DateModified)
                {
                    DateTime dateAfterChange = DateTime.Parse(change.ChangedValue);
                    ret.RemoveAll(x => x.Item1.Date == change.CourseTime.Date);
                    DateTime courseStart = dateAfterChange.AddHours(course.StartTime.Hours).AddMinutes(course.StartTime.Minutes);
                    DateTime courseEnd = dateAfterChange.AddHours(course.EndTime.Hours).AddMinutes(course.EndTime.Minutes);
                    ret.Add(new Tuple<DateTime, DateTime>(courseStart, courseEnd));
                }
                else if ((CourseChangeType)change.ChangeType == CourseChangeType.TimeModified)
                {
                    DateTime startTimeAfterChange = DateTime.Parse(change.ChangedValue.Split('-')[0]);
                    DateTime endTimeAfterChange = DateTime.Parse(change.ChangedValue.Split('-')[1]);
                    ret.RemoveAll(x => x.Item1.Date == change.CourseTime.Date);
                    DateTime courseStart = change.CourseTime.Date.AddHours(startTimeAfterChange.Hour).AddMinutes(startTimeAfterChange.Minute);
                    DateTime courseEnd = change.CourseTime.Date.AddHours(endTimeAfterChange.Hour).AddMinutes(endTimeAfterChange.Minute);
                    ret.Add(new Tuple<DateTime, DateTime>(courseStart, courseEnd));
                }
                else if ((CourseChangeType)change.ChangeType == CourseChangeType.Cancelled)
                {
                    ret.RemoveAll(x => x.Item1.Date == change.CourseTime.Date);
                }
            }

            return ret;
        }

        private int GetWeekIndex(DateTime now,DateTime start)
        {
            var dif = (int)(now - start).TotalDays;
            return dif / 7;
        }
        
    }
}
