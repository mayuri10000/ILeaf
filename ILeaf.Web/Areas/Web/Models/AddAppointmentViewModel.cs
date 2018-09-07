using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ILeaf.Web.Areas.Web.Models
{
    [MetadataType(typeof(AddAppointmentViewModel))]
    public class AddAppointmentViewModel : BaseViewModel
    {
        [DisplayName("日程标题")]
        [Required(ErrorMessage = "日程标题不能为空")]
        [MaxLength(50, ErrorMessage = "日程标题不得超过50个字符")]
        public string Title { get; set; }

        [DisplayName("开始日期")]
        [Required(ErrorMessage = "开始日期不能为空")]
        [RegularExpression("^[1-9]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$",ErrorMessage = "日期格式不正确")]
        public string StartDate { get; set; }
        
        [DisplayName("开始时间")]
        public string StartTime { get; set; }

        [DisplayName("结束日期")]
        [RegularExpression("^[1-9]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])$", ErrorMessage = "日期格式不正确")]
        public string EndDate { get; set; }

        [DisplayName("结束时间")]
        public string EndTime { get; set; }

        [DisplayName("全天")]
        [Required]
        public bool IsAllDay { get; set; }

        [DisplayName("地点")]
        [MaxLength(50, ErrorMessage = "地点信息不得超过50个字符")]
        public string Place { get; set; }

        [DisplayName("详情")]
        public string Details { get; set; }

        [DisplayName("可见性")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "输入无效")]
        public string Visiblity { get; set; }

        public string ShareInfo { get; set; }

        public string Id { get; set; }
        public string Json { get; set; }

        public List<Account> Friends { get; set; }
        public List<Account> Classmates { get; set; }
        public List<Group> Groups { get; set; } 
    }
}