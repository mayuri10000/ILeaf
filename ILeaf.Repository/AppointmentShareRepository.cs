using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAppointmentShareRepository : IBaseRepository<AppointmentShare>
    {
    }

    public class AppointmentShareRepository : BaseRepository<AppointmentShare>, IAppointmentShareRepository
    {
    }
}
