using ILeaf.Core.Models;
using ILeaf.Repository;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ILeaf.Service
{
    public interface IAppointmentServise : IBaseService<Appointment>
    {
        List<Appointment> GetPersonalAppointments(long userId);
        List<Appointment> GetGroupAppointments(long groupId);
        List<Appointment> GetFriendAppointment(long userId);
        void SendAppointmentInvition(long appointmentId, long receiverUserID);
        List<AppointmentShare> GetAllAppointmentInvition(long userId);
        void AcceptAppointmentInvition(long appointmentId, long senderUserID, long receiverUserID);
        void DeclineAppointmentInvition(long appointmentId, long senderUserID, long receiverUserID);
        void SendAppointmentToGroup(long appointmentId, long groupId);
    }

    public class AppointmentService : BaseService<Appointment>, IAppointmentServise
    {
        private IAppointmentShareRepository share_repo = StructureMap.ObjectFactory.GetInstance<IAppointmentShareRepository>();

        public AppointmentService(IBaseRepository<Appointment> repo) : base(repo)
        {
        }

        public void AcceptAppointmentInvition(long appointmentId, long senderUserID, long receiverUserID)
        {
            AppointmentShare share = share_repo.GetFirstOrDefaultObject(s => s.AppointmentId == appointmentId && s.ShareToId == receiverUserID
              && s.Appointment.CreaterUserId == senderUserID && !s.IsShareToGroup && !s.IsAccepted);
            share.IsAccepted = true;
            share_repo.Save(share);
        }

        public void DeclineAppointmentInvition(long appointmentId, long senderUserID, long receiverUserID)
        {
            AppointmentShare share = share_repo.GetFirstOrDefaultObject(s => s.AppointmentId == appointmentId && s.ShareToId == receiverUserID
              && s.Appointment.CreaterUserId == senderUserID && !s.IsShareToGroup && !s.IsAccepted);
            share_repo.Delete(share);
        }

        public List<AppointmentShare> GetAllAppointmentInvition(long userId)
        {
            var share = share_repo.GetObjectList(s => s.ShareToId == userId
              && !s.IsShareToGroup && !s.IsAccepted, s => s.AppointmentId, Core.Enums.OrderingType.Descending, 0, 0);
            return share;
        }

        public List<Appointment> GetFriendAppointment(long userId)
        {
            var share = share_repo.GetObjectList(s => s.ShareToId == userId
                && !s.IsShareToGroup && s.IsAccepted, s => s.AppointmentId, Core.Enums.OrderingType.Descending, 0, 0);
            return (from AppointmentShare s in share where true select s.Appointment).ToList();
        }

        public List<Appointment> GetGroupAppointments(long groupId)
        {
            var share = share_repo.GetObjectList(s => s.ShareToId == groupId
                && s.IsShareToGroup , s => s.AppointmentId, Core.Enums.OrderingType.Descending, 0, 0);
            return (from AppointmentShare s in share where true select s.Appointment).ToList();
        }

        public List<Appointment> GetPersonalAppointments(long userId)
        {
            var appointments = GetFullList(a => a.CreaterUserId == userId, a => a.Id, Core.Enums.OrderingType.Descending);
            return appointments;
        }

        public void SendAppointmentInvition(long appointmentId, long receiverUserID)
        {
            AppointmentShare share = new AppointmentShare()
            {
                AppointmentId = appointmentId,
                IsShareToGroup = false,
                IsAccepted = false,
                ShareToId = receiverUserID
            };
            share_repo.Save(share);
        }

        public void SendAppointmentToGroup(long appointmentId, long groupId)
        {
            Appointment a = GetObject(appointmentId);
            IGroupService groupService = StructureMap.ObjectFactory.GetInstance<IGroupService>();
            Group group = groupService.GetObject(groupId);
            if (group.HeadmanId != null && group.HeadmanId != a.CreaterUserId)
                throw new Exception("只有小组组长才能创建小组日程！");

            else if ((group.Type == (byte)Core.Enums.GroupType.Class || group.Type == (byte)Core.Enums.GroupType.ClassGroup)
                    && a.Creater.UserType != (byte)Core.Enums.UserType.Teacher)
                throw new Exception("只有教师、辅导员才能添加班级日程");

            else if (group.Type == (byte)Core.Enums.GroupType.School)
                throw new Exception("不能添加全校日程");

            AppointmentShare share = new AppointmentShare()
            {
                AppointmentId = appointmentId,
                IsShareToGroup = true,
                IsAccepted = true,
                ShareToId = groupId
            };
            share_repo.Save(share);
        }
    }
}
