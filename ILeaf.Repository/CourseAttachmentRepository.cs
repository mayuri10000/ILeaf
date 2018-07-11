using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public class CourseAttachmentRepository : BaseRepository<CourseAttachment> , ICourseAttachmentRepository
    {
    }

    public interface ICourseAttachmentRepository : IBaseRepository<CourseAttachment>
    {
    }
}
