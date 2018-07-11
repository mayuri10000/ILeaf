using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IGroupMemberRepository : IBaseRepository<GroupMember>
    {
    }

    public class GroupMemberRepository : BaseRepository<GroupMember>, IGroupMemberRepository
    {
    }
}
