using ILeaf.Core.Models;
using ILeaf.Web.Models;

namespace ILeaf.Web.Areas.Web.Models
{
    public class AddCourseViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Classroom { get; set; }
        public bool IsSelective { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Weeks { get; set; }
        public string Weekday { get; set; }
        public string Classes { get; set; }
        public string SemesterStart { get; set; }

        public Course Course { get; set; }
        public string Id { get; set; }
    }
}