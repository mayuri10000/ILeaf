using ILeaf.Web.Models;
using ILeaf.Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ILeaf.Web.Areas.Web.Models
{
    [MetadataType(typeof(RegisterViewModel))]
    public class RegisterViewModel : BaseViewModel
    {
        [DisplayName("用户名")]
        [System.Web.Mvc.Remote("CheckIfUserNameOrEmailExist", "Login", ErrorMessage = "该用户名已被注册,请换一个")]
        [Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(20, ErrorMessage = "用户名不能超过20个字符")]
        [MinLength(6, ErrorMessage = "用户名不能小于6个字符")]
        [RegularExpression("^[A-Za-z0-9\\u0391-\\uFFE5]+$", ErrorMessage = "用户名中不得包含特殊字符")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [MinLength(6, ErrorMessage = "密码不能少于6个字符")]
        [MaxLength(20, ErrorMessage = "密码不能超过20个字符")]
        [RegularExpression("^((?![^\\x00-\\xff]).)*$", ErrorMessage = "密码中不得包含全角字符(如汉字)")]
        public string Password { get; set; }

        [DisplayName("确认密码")]
        [Compare("Password", ErrorMessage = "两次密码输入不一致")]
        [Required(ErrorMessage = "请重新输入一次密码")]
        public string ReTypePassword { get; set; }

        [DisplayName("电子邮箱")]
        [EmailAddress(ErrorMessage = "邮箱地址格式有误")]
        [System.Web.Mvc.Remote("CheckIfUserNameOrEmailExist", "Login", ErrorMessage = "该邮箱地址已被注册")]
        [Required(ErrorMessage = "请输入邮箱地址")]
        public string EMail { get; set; }

        [DisplayName("真实姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }

        [DisplayName("性别")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "请选择性别")]
        [Required]
        public string Gender { get; set; }

        [DisplayName("学校名称")]
        [Required(ErrorMessage = "请选择学校")]
        [System.Web.Mvc.Remote("CheckSchoolName", "Login", ErrorMessage = "该学校不存在")]
        public string SchoolName { get; set; }

        [DisplayName("班级")]
        [Required(ErrorMessage = "请选择班级")]
        public string ClassName { get; set; }

        [DisplayName("卡号")]
        [Required(ErrorMessage = "请输入校园卡号")]
        public string SchoolCardNum { get; set; }

        [DisplayName("用户类型")]
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "请选择用户类型")]
        public string UserType { get; set; }

        [DisplayName("验证码")]
        [Required(ErrorMessage = "请输入验证码")]
        public string VerificationCode { get; set; }

    }
}