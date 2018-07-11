using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
    }

    public class AppointmentRepository : BaseRepository<Appointment> , IAppointmentRepository
    {
    }
}
