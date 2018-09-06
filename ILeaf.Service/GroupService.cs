using ILeaf.Core;
using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using ILeaf.Repository;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILeaf.Service
{
    public interface IGroupService : IBaseService<Group>
    {
        void CreateGroup(string groupName);
        void AddMember(long groupId, long memberId);
        List<Group> GetGroups();
        Group GetGroupByName(string name);
        void SendGroupJoinRequest(long groupId, long userId);
        void AcceptGroupJoinRequest(long groupId, long userId);
        void DeclineGroupJoinRequest(long groupId, long userId);
        void DeleteMember(long groupId, long userId);
        void DeleteGroup(long groupId);
    }

    public class GroupService : BaseService<Group>, IGroupService
    { 
        public GroupService(IGroupRepository repo) : base(repo) { }

        public IGroupMemberRepository member_repo = ObjectFactory.GetInstance<IGroupMemberRepository>();

        public void AcceptGroupJoinRequest(long groupId, long userId)
        {
            if (!IsHeadman(groupId))
                throw new Exception("只有组长才能同意进组请求");
            GroupMember m = member_repo.GetFirstOrDefaultObject(g => g.GroupId == groupId && g.MemberId == userId);
            m.IsAccepted = true;
            member_repo.Save(m);
        }

        public List<Group> GetGroups()
        {
            Account account = Server.HttpContext.Session["Account"] as Account;
            var members = member_repo.GetObjectList(x => x.MemberId == account.Id && x.IsAccepted, x => x.GroupId, OrderingType.Ascending, 0, 0).ConvertAll(x => x.Group).ToList();
            return members;
        }

        private bool IsHeadman(long groupId)
        {
            Group gp = GetObject(groupId);
            Account account = Server.HttpContext.Session["Account"] as Account;
            return gp.HeadmanId == account.Id;
        }

        public void AddMember(long groupId, long memberId)
        {
            GroupMember m = new GroupMember()
            {
                GroupId = groupId,
                MemberId = memberId,
                IsAccepted = true,
            };
            
            if (!IsHeadman(groupId))
                throw new Exception("只有组长才能添加组员");
            member_repo.Save(m);
        }

        public void CreateGroup(string groupName)
        {
            Account account = Server.HttpContext.Session["Account"] as Account;
            Group g = new Group()
            {
                Name = groupName,
                HeadmanId = account.Id
            };
            BaseRepository.Save(g);
        }

        public void DeclineGroupJoinRequest(long groupId, long userId)
        {
            GroupMember x = member_repo.GetFirstOrDefaultObject(m => m.GroupId == groupId && m.MemberId == userId && !m.IsAccepted);
            if (x == null)
                throw new Exception("进组请求不存在！");
            if (!IsHeadman(groupId))
                throw new Exception("只有组长才能进行此操作");
            member_repo.Delete(x);
        }

        public void DeleteGroup(long groupId)
        {
            Group g = BaseRepository.GetFirstOrDefaultObject(x => x.Id == groupId);
            if (g == null)
                throw new Exception("小组信息不存在！");
            if (!IsHeadman(groupId))
                throw new Exception("只有组长才能进行此操作");


            var members = member_repo.GetObjectList(x => x.GroupId == groupId, x => x.GroupId, OrderingType.Ascending, 0, 0);
            foreach(var m in members)
            {
                member_repo.Delete(m);
            }
            BaseRepository.Delete(g);
        }

        public void DeleteMember(long groupId, long userId)
        {
            GroupMember x = member_repo.GetFirstOrDefaultObject(m => m.GroupId == groupId && m.MemberId == userId);
            if (!IsHeadman(groupId))
                throw new Exception("只有组长才能进行此操作");
            member_repo.Delete(x);
        }

        public Group GetGroupByName(string name)
        {
            return BaseRepository.GetFirstOrDefaultObject(x => x.Name == name);
        }

        public void SendGroupJoinRequest(long groupId, long userId)
        {
            GroupMember m = new GroupMember()
            {
                GroupId = groupId,
                MemberId = userId,
                IsAccepted = false,
            };
            member_repo.Save(m);
        }

        public override void SaveObject(Group obj)
        {
            if (!IsHeadman(obj.Id))
                throw new Exception("只有组长才能进行此操作");
            base.SaveObject(obj);
        }
    }
}
