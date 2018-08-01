using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAppointmentShareToUserRepository : IBaseRepository<AppointmentShareToUser>
    {
    }

    public class AppointmentShareToUserRepository : BaseRepository<AppointmentShareToUser>, IAppointmentShareToUserRepository
    {
    }
}
