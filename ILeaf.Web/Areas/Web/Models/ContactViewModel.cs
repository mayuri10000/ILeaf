using ILeaf.Core.Models;
using ILeaf.Web.Models;
using System.Collections.Generic;

namespace ILeaf.Web.Areas.Web.Models
{
    public class ContactViewModel : BaseViewModel
    {
        public List<Account> Friends { get; set; }
        public List<Account> Classmates { get; set; }
        public List<Account> PendingFriends { get; set; }
        public Account CurrentAccount { get; set; }
        public List<Group> Groups { get; set; }
        public bool IsSelf { get; set; }
        public bool IsFriend { get; set; }
        public bool IsClassmate { get; set; }
        public bool IsPendingFriend { get; set; }
    }
}