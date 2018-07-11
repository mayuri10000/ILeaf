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
        void CreateGroup(string groupName, GroupType type);
        void AddMember(long groupId, long memberId);
        void AddSubGroup(long groupId, long subGroupId);
        Group GetGroupById(long id);
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
            GroupMember m = member_repo.GetFirstOrDefaultObject(g => g.GroupId == groupId && g.MemberId == userId && !g.IsMemberGroup);
            m.IsAccepted = true;
            member_repo.Save(m);
        }

        public void AddMember(long groupId, long memberId)
        {
            GroupMember m = new GroupMember()
            {
                GroupId = groupId,
                MemberId = memberId,
                IsAccepted = true,
                IsMemberGroup = false
            };
            member_repo.Save(m);
        }

        public void AddSubGroup(long groupId, long subGroupId)
        {
            GroupMember m = new GroupMember()
            {
                GroupId = groupId,
                MemberId = subGroupId,
                IsAccepted = true,
                IsMemberGroup = true
            };
            member_repo.Save(m);
        }

        public void CreateGroup(string groupName, GroupType type)
        {
            Group g = new Group()
            {
                Name = groupName,
                Type = (byte)type
            };
            BaseRepository.Save(g);
        }

        public void DeclineGroupJoinRequest(long groupId, long userId)
        {
            GroupMember x = member_repo.GetFirstOrDefaultObject(m => m.GroupId == groupId && m.MemberId == userId && !m.IsAccepted && !m.IsMemberGroup);
            member_repo.Delete(x);
        }

        public void DeleteGroup(long groupId)
        {
            Group g = BaseRepository.GetFirstOrDefaultObject(x => x.Id == groupId);
            BaseRepository.Delete(g);
        }

        public void DeleteMember(long groupId, long userId)
        {
            GroupMember x = member_repo.GetFirstOrDefaultObject(m => m.GroupId == groupId && m.MemberId == userId && !m.IsMemberGroup);
            member_repo.Delete(x);
        }

        public Group GetGroupById(long id)
        {
            return BaseRepository.GetFirstOrDefaultObject(x => x.Id == id);
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
                IsMemberGroup = false
            };
            member_repo.Save(m);
        }
    }
}
