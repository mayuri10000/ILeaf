using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System.Collections.Generic;

namespace ILeaf.Web.Areas.Web.Models
{
    public class NotificationsViewModel : BaseViewModel
    {
        public List<Notification> Notifications { get; set; }
    }
}