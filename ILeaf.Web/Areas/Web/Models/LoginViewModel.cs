using ILeaf.Web.Models;

namespace ILeaf.Web.Areas.Web.Models
{
    public class LoginViewModel : BaseViewModel
    {
        new public string UserName { get; set; }
        public string Password { get; set; }
        public string CheckCode { get; set; }
        public bool ShowCheckCode { get; set; }
        public string ReturnUrl { get; set; }
        public bool AuthorityNotReach { get; set; }
        public bool IsLogined { get; set; }
    }
}