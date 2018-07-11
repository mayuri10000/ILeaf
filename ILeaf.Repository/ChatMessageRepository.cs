using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
    }

    public interface IChatMessageRepository : IBaseRepository<ChatMessage>
    {
    }
}
