using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAttachmentAccessRepository : IBaseRepository<AttachmentAccess>
    {
    }

    public class AttachmentAccessRepository : BaseRepository<AttachmentAccess> , IAttachmentAccessRepository
    {
    }
}
