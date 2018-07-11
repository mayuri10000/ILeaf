using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {
    }

    public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
    }
}
