using ILeaf.Core.Extensions;
using ILeaf.Web.Models;

namespace System.Web.Mvc
{
    public static class CurrentMenuExtensions
    {
        public static string CurrentMenu(this HtmlHelper htmlHelper, string menuName, string className = "active",string defaultClassName="panel")
        {
            if (htmlHelper.ViewData.Model is BaseViewModel)
            {
                BaseViewModel model = htmlHelper.ViewData.Model as BaseViewModel;
                if (!model.CurrentMenu.IsNullOrEmpty())
                {
                    //int indexOf = model.CurrentMenu.LastIndexOf('.');
                    //string parentMenuMane = model.CurrentMenu.Substring(0, indexOf);
                    var parentMenuMane = model.CurrentMenu.Split('.')[0];
                    if (model.CurrentMenu.StartsWith(menuName, StringComparison.OrdinalIgnoreCase)
                           || parentMenuMane.Equals(menuName, StringComparison.OrdinalIgnoreCase))
                    {
                        return defaultClassName + " "+className;
                    }
                    else
                    {
                        return defaultClassName;
                    }
                }
                else
                {
                    return defaultClassName;
                }
            }
            else
            {
                return defaultClassName;
            }
        }
    }
}
