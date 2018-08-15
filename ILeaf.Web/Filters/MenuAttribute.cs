using ILeaf.Web.Models;
using System.Web.Mvc;

namespace ILeaf.Web.Filters
{
    public class MenuAttribute : ActionFilterAttribute
    {
        public string CurrentMenu { get; set; }
        public MenuAttribute(string currentMenu)
        {
            CurrentMenu = currentMenu;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.Model is BaseViewModel)
            {
                BaseViewModel model = filterContext.Controller.ViewData.Model as BaseViewModel;
                model.CurrentMenu = model.CurrentMenu ?? CurrentMenu;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}