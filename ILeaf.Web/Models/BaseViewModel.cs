/*  BaseViewModel.cs  视图模型基类
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace ILeaf.Web.Models
{
    /// <summary>
    /// 项目中几乎所有视图模型基类
    /// </summary>
    public class BaseViewModel
    {
        public Account Account { get; set; }
        public RouteData RouteData { get; set; }
        public string CurrentMenu { get; set; }
        public List<Messager> MessagerList { get; set; }
        public DateTime PageStartTime { get; set; }
        public DateTime PageEndTime { get; set; }
    }

    /// <summary>
    /// 成功提示页面模型
    /// </summary>
    public class SuccessViewModel : BaseViewModel
    {
        public string Message { get; set; }
        public string BackUrl { get; set; }
        public string BackAction { get; set; }
        public string BackController { get; set; }
        public RouteValueDictionary BackRouteValues { get; set; }
    }
}