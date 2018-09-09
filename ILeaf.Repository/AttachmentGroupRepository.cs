using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAttachmentGroupRepository : IBaseRepository<AttachmentGroup>
    {
    }

    public class AttachmentGroupRepository : BaseRepository<AttachmentGroup>, IAttachmentGroupRepository
    {
    }
}
