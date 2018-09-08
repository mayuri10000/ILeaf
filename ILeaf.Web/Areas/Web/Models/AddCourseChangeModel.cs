using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILeaf.Web.Areas.Web.Models
{
    public class AddCourseChangeModel : BaseViewModel
    {
        public long CourseId { get; set; }
        public string ChangeType { get; set; }
        public string ChangeValue { get; set; }
        public string CourseTime { get; set; }
    }
}