using ILeaf.Web.Models;

namespace ILeaf.Web.Areas.Web.Models
{
    public class AddCourseChangeModel : BaseViewModel
    {
        public long CourseId { get; set; }
        public string ChangeType { get; set; }
        public string ChangeValue { get; set; }
        public string CourseTime { get; set; }

        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}