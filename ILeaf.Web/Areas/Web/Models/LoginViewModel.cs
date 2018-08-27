using ILeaf.Web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ILeaf.Web.Areas.Web.Models
{
    [MetadataType(typeof(LoginViewModel))]
    public class LoginViewModel : BaseViewModel
    {
        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不可为空")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [PasswordPropertyText]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        [DisplayName("验证码")]
        public string VerificationCode { get; set; }

        public bool ShowVerificationCode { get; set; }

        public string ReturnUrl { get; set; }

        public bool AuthorityNotReach { get; set; }

        public bool IsLogined { get; set; }
    }
}