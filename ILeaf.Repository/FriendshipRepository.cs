using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IFriendshipRepository : IBaseRepository<Friendship>
    {

    }

    public class FriendshipRepository : BaseRepository<Friendship>, IFriendshipRepository
    {
    }
}
