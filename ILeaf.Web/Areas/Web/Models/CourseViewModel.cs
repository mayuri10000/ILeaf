using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILeaf.Web.Areas.Web.Models
{
    public class CourseViewModel : BaseViewModel
    {
        public ICollection<Course> Courses { get; set; }
        public Course CurrentCourse { get; set; }
        public bool IsTeacherOfThis { get; set; }
        public ICollection<CourseChange> Changes { get; set; }
        public ICollection<AttachmentCourse> Attachments { get; set; }
    }
}