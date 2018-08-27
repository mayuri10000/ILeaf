using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System.Collections.Generic;

namespace ILeaf.Web.Areas.Web.Models
{
    public class CalendarViewModel : BaseViewModel
    {
        public List<Appointment> Appointments { get; set; }
    }
}