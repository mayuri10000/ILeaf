using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public class ClassInfoRepository : BaseRepository<ClassInfo>, IClassInfoRepository
    {
    }

    public interface IClassInfoRepository : IBaseRepository<ClassInfo>
    {
    }
}
