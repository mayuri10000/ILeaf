using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface ICourseClassRepository : IBaseRepository<CourseClass>
    {
    }

    public class CourseClassRepository : BaseRepository<CourseClass> , ICourseClassRepository
    {
    }
}
