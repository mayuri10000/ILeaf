using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
    }

    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
    }
}
