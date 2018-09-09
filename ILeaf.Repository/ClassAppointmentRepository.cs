using ILeaf.Core.Models;

namespace ILeaf.Repository
{

    public interface IClassAppointmentRepository : IBaseRepository<ClassAppointment>
    {
    }

    public class ClassAppointmentRepository : BaseRepository<ClassAppointment>, IClassAppointmentRepository
    {
    }
}
