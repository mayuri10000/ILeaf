using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface ISelectableCourseStudentRepository : IBaseRepository<SelectableCourseStudent>
    {
    }

    public class SelectableCourseStudentRepository : BaseRepository<SelectableCourseStudent>, ISelectableCourseStudentRepository
    {
    }
}
