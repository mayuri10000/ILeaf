using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;

namespace ILeaf.Service
{
    public interface ICourseService : IBaseService<Course>
    {
        void AddCourseChangeInfo(long courseId, DateTime time, IDictionary<string, string> changedValues, string description = "");
        List<Tuple<DateTime,DateTime>> GetAllTimeForCourse(long courseId);
        void AddAttachmentForCourse(long courseId, long attachmentId, DateTime time);
        bool HasCourseAtTime(long courseId, DateTime time);
        List<Course> GetCoursesForClass(long classId);
        List<Course> GetAllCourseForUser(long userId);
    }

    public class CourseService : BaseService<Course>, ICourseService
    {
        ICourseChangeRepository courseChangeRepository = ObjectFactory.GetInstance<ICourseChangeRepository>();

        public CourseService(ICourseRepository repo) : base(repo) { }

        public void AddAttachmentForCourse(long courseId, long attachmentId, DateTime time)
        {
            ICourseAttachmentRepository courseAttachment = ObjectFactory.GetInstance<ICourseAttachmentRepository>();
            CourseAttachment att = new CourseAttachment()
            {
                AttachmentId = attachmentId,
                Time = time,
                CourseId = courseId,
            };
            courseAttachment.Save(att);

        }

        public void AddCourseChangeInfo(long courseId,DateTime time, IDictionary<string, string> changedValues, string description = "")
        {
            string strChangeInfo = (String.Concat(from k in changedValues where true select k.Key + ":" + k.Value + "|") + ">").Replace("|>", "");
            CourseChange change = new CourseChange()
            {
                CourseId = courseId,
                Time = time,
                SubmitTime = DateTime.Now,
                CahngeInfo = strChangeInfo,
                Description = description
            };
            courseChangeRepository.Save(change);
        }

        public List<Course> GetAllCourseForUser(long userId)
        {
            IAccountService accountService = ObjectFactory.GetInstance<IAccountService>();
            Account account = accountService.GetObject(userId);
            if (account.UserType == (byte)UserType.Teacher)
                return GetFullList(c => c.TeacherId == userId && c.TermStart < DateTime.Now && c.TermEnd > DateTime.Now, c => c.Id, OrderingType.Ascending);
            else if (account.UserType == (byte)UserType.Student && account.ClassId != null)
                return GetCoursesForClass(account.ClassId.Value);

            return new List<Course>();
        }

        public List<Tuple<DateTime, DateTime>> GetAllTimeForCourse(long courseId)
        {
            Course course = GetObject(courseId);
            List<Tuple<DateTime, DateTime>> ret = new List<Tuple<DateTime, DateTime>>();

        }

        public List<Course> GetCoursesForClass(long classId)
        {
            throw new NotImplementedException();
        }

        public bool HasCourseAtTime(long courseId, DateTime time)
        {
            throw new NotImplementedException();
        }
    }
}
