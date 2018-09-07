using ILeaf.Core;
using ILeaf.Core.Models;
using ILeaf.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ILeaf.Service
{
    public interface IFriendshipService : IBaseService<Friendship>
    {
        List<Account> GetAllFriend();
        List<Account> GetPendingFriendRequests();
        void SendFriendRequestTo(long userId);
        void AcceptFriendshipRequestFrom(long userId);
        void DeclineFriendshipRequestFromOrRemoveFriend(long userId);
        List<Account> GetSentButNotAcceptedFriendRequests();
    }

    public class FriendshipService:BaseService<Friendship>,IFriendshipService
    {
        public FriendshipService(FriendshipRepository repo) : base(repo) { }

        public List<Account> GetAllFriend()
        {
            Account self = Server.HttpContext.Session["Account"] as Account;
            List<Friendship> friendships = GetObjectList(0, 0, x => (x.Account1 == self.Id || x.Account2 == self.Id) && x.IsAccepted, 
                x => 0, Core.Enums.OrderingType.Ascending);
            return (from f in friendships select f.Account1 == self.Id ? f.User2 : f.User1).ToList();
        }

        public List<Account> GetPendingFriendRequests()
        {
            Account self = Server.HttpContext.Session["Account"] as Account;
            List<Friendship> friendships = GetObjectList(0, 0, x => x.Account2 == self.Id && !x.IsAccepted,
                x => 0, Core.Enums.OrderingType.Ascending);
            return (from f in friendships select f.User1).ToList();
        }

        public List<Account> GetSentButNotAcceptedFriendRequests()
        {
            Account self = Server.HttpContext.Session["Account"] as Account;
            List<Friendship> friendships = GetObjectList(0, 0, x => x.Account1 == self.Id && !x.IsAccepted,
                x => 0, Core.Enums.OrderingType.Ascending);
            return (from f in friendships select f.User2).ToList();
        }

        public void SendFriendRequestTo(long userId)
        {
            Account user1 = Server.HttpContext.Session["Account"] as Account;
            Friendship friendship = new Friendship()
            {
                Account1 = user1.Id,
                Account2 = userId,
                IsAccepted = false,
            };
            SaveObject(friendship);
        }

        public void AcceptFriendshipRequestFrom(long userId)
        {
            Account user2 = Server.HttpContext.Session["Account"] as Account;
            Friendship friendship = GetObject(f => f.Account2 == user2.Id && f.Account1 == userId && !f.IsAccepted);
            friendship.IsAccepted = true;
            SaveObject(friendship);
        }

        public void DeclineFriendshipRequestFromOrRemoveFriend(long userId)
        {
            Account user2 = Server.HttpContext.Session["Account"] as Account;
            Friendship friendship = GetObject(f => (f.Account2 == user2.Id && f.Account1 == userId)
                || (f.Account1 == user2.Id && f.Account2 == userId));
            DeleteObject(friendship);
        }


    }
}
