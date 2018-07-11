using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;

namespace ILeaf.Service
{
    public interface ICourseService : IBaseService<Course>
    {
        void AddCourseChangeInfo(long courseId, CourseChangeType changeType, object changedValue);
        List<DateTime> GetAllTimeForCourse(long courseId);
        void AddAttachmentForCourse(long courseId, Attachment attachment, DateTime time);
        bool HasCourseAtTime(long courseId, DateTime time);
        List<Course> GetCoursesForClass(long classId);

    }

    public class CourseService : BaseService<Course>, ICourseService
    {
        public CourseService(ICourseRepository repo) : base(repo) { }

        public void AddAttachmentForCourse(long courseId, Attachment attachment, DateTime time)
        {
            throw new NotImplementedException();
        }

        public void AddCourseChangeInfo(long courseId, CourseChangeType changeType, object changedValue)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetAllTimeForCourse(long courseId)
        {
            throw new NotImplementedException();
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
