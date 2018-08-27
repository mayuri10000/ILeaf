using ILeaf.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace ILeaf.Web.Areas.Web.Models
{
    [MetadataType(typeof(ForgetPasswordViewModel))]
    public class ForgetPasswordViewModel : BaseViewModel
    {
        [EmailAddress(ErrorMessage = "请输入有效的邮件地址")]
        [Required(ErrorMessage = "请输入邮件地址")]
        public string EMail { get; set; }
    }
}