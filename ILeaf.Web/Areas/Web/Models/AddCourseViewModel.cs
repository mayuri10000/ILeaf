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
        public string Classes { get; set; }
    }
}