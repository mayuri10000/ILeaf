using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
    }

    public class GroupRepository : BaseRepository<Group> , IGroupRepository
    {
    }
}
