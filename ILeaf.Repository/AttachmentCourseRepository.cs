using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAttachmentCourseRepository : IBaseRepository<AttachmentCourse>
    {
    }

    public class AttachmentCourseRepository : BaseRepository<AttachmentCourse> , IAttachmentCourseRepository
    {
    }
}
