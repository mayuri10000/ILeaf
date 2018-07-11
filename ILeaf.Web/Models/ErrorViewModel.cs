/*  ErrorViewModel.cs  错误页面视图模型
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using System.Web.Mvc;

namespace ILeaf.Web.Models
{
    public class BaseErrorViewModel : BaseViewModel { }

    public class ExceptionViewModel : BaseErrorViewModel
    {
        public HandleErrorInfo HandleErrorInfo { get; set; }
    }

    public class HttpErrorViewModel : BaseErrorViewModel
    {
        public int ErrCode { get; set; }
        public string Url { get; set; }
    }
}