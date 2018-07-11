using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
    }

    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
    }
}
