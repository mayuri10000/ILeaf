using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAttachmentClassRepository : IBaseRepository<AttachmentClass>
    {
    }

    public class AttachmentClassRepository : BaseRepository<AttachmentClass> ,IAttachmentClassRepository
    {
    }
}
