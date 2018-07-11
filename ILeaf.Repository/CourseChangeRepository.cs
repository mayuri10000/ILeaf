using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public class CourseChangeRepository : BaseRepository<CourseChange>, ICourseChangeRepository
    {
    }

    public interface ICourseChangeRepository : IBaseRepository<CourseChange>
    {
    }
}
