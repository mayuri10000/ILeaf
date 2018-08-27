using ILeaf.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ILeaf.Web.Areas.Web.Models
{
    [MetadataType(typeof(AddAppointmentViewModel))]
    public class AddAppointmentViewModel : BaseViewModel
    {
        [DisplayName("日程标题")]
        [Required(ErrorMessage = "日程标题不能为空")]
        public string Title { get; set; }

        [DisplayName("开始时间")]
        [Required(ErrorMessage = "开始时间不能为空")]
        public string StartTime { get; set; }

        [DisplayName("结束时间")]
        public string EndTime { get; set; }

        [DisplayName("全天")]
        [Required]
        public bool IsAllDay { get; set; }

        [DisplayName("地点")]
        public string Place { get; set; }

        [DisplayName("详情")]
        public string Details { get; set; }

        [DisplayName("分享给用户")]
        public string ShareUsers { get; set; }
    }
}