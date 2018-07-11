using ILeaf.Core.Models;

namespace ILeaf.Repository
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
    }

    public interface INotificationRepository : IBaseRepository<Notification>
    {
    }
}
