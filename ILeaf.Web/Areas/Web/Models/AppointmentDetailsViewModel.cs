using ILeaf.Core.Models;
using ILeaf.Web.Models;

namespace ILeaf.Web.Areas.Web.Models
{
    public class AppointmentDetailsViewModel : BaseViewModel
    {
        public Appointment Appointment { get; set; }
    }
}