using ILeaf.Web.Controllers;
using ILeaf.Web.Models;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    public class ChatController : BaseController
    {
        // GET: Web/Chat
        public ActionResult Index()
        {
            return View(new BaseViewModel()
            {
                Account = Account,
            });
        }
    }
}