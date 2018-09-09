using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IGroupAppointmentRepository : IBaseRepository<GroupAppointment>
    {
    }

    public class GroupAppointmentRepository : BaseRepository<GroupAppointment>, IGroupAppointmentRepository
    {
    }
}
