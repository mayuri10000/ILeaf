using ILeaf.Core;
using ILeaf.Core.Models;
using ILeaf.Core.Utilities;
using ILeaf.Repository;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ILeaf.Service
{
    public interface IAttachmentService : IBaseService<Attachment>
    {
        Attachment SaveAttachment(HttpPostedFileBase file);
        bool HasAccess(long attachmentId);
        void DeleteAttachment(long attachmentId);
        void DeleteAllExpiredAttachment();
        void UpdateAttachment(long attachmentId, HttpPostedFileBase file);
        void GiveAccessToUser(long attachmentId, long userId);
        void GiveAccessToGroup(long attachmentId, long groupId);
        void GiveAccessToClass(long attachmentId, long classId);
        void RemoveAccessForUser(long attachmentId, long userId);
        void RemoveAccessForGroup(long attachmentId, long groupId);
        void RemoveAccessForClass(long attachmentId, long classId);
        void SetPublic(long attachmentId, bool isPublic);
    }

    public class AttachmentService : BaseService<Attachment>, IAttachmentService
    {
        public AttachmentService(IAttachmentRepository repo) : base(repo)
        {
        }

        public Attachment SaveAttachment(HttpPostedFileBase file)
        {
            string filename =  Guid.NewGuid().ToString();

            Upload.UploadFile("~/Upload/Attachments/", filename, file, 1024 * 100, false);

            Attachment a = new Attachment()
            {
                UploaderId = ((Account)Server.HttpContext.Session["Account"]).Id,
                FileName = file.FileName,
                FileSize = file.ContentLength,
                StoragePath = Server.GetMapPath("~/Upload/Attachments/" + filename),
                UploadTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddDays(30),
                IsPublicAttachment = false,
            };

            SaveObject(a);

            return a;
        }

        public bool HasAccess(long attachmentId)
        {
            Attachment attachment = GetObject(attachmentId);
            if (attachment == null)
                throw new Exception("附件不存在或已过期");

            Account account = Server.HttpContext.Session["Account"] as Account;
            
            bool userHaveAccess = attachment.AccessableUsers.Contains(account) || attachment.UploaderId.Equals(account.Id);
            bool groupHaveAccess = (from g in account.BelongtoGroups
                                    where attachment.AccessableGroups.Where(x => x.Id == g.GroupId).FirstOrDefault() != null
                                    select g).FirstOrDefault() != null;
            bool classHaveAccess = account.UserType == 2 && attachment.AttachmentCourses.ToList().ConvertAll(x => x.Course)
                .Where(x => x.Classes.Where(y => y.Id == account.ClassId).FirstOrDefault() != null).FirstOrDefault() != null;

            return userHaveAccess || groupHaveAccess || classHaveAccess;
        }

        public void DeleteAttachment(long attachmentId)
        {
            Attachment a = GetObject(attachmentId);
            DeleteObject(a);
            File.Delete(a.StoragePath);
        }

        public void DeleteAllExpiredAttachment()
        {
            var attachemnts = GetFullList(a => a.ExpireTime < DateTime.Now, a => a.Id, Core.Enums.OrderingType.Descending);
            foreach (Attachment a in attachemnts)
                DeleteAttachment(a.Id);
        }

        public void UpdateAttachment(long attachmentId, HttpPostedFileBase file)
        {
            Attachment a = GetObject(attachmentId);
            Upload.UploadFile("~/Upload/Attachments/", Path.GetFileName(a.StoragePath), file, 1024 * 100, true);
                
            a.FileName = file.FileName;
            a.ExpireTime = DateTime.Now.AddDays(30);
            a.FileSize = file.ContentLength;

            SaveObject(a);
        }
        

        public void GiveAccessToUser(long attachmentId, long userId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");
            
            Account account = StructureMap.ObjectFactory.GetInstance<IAccountRepository>().GetObjectById(userId);
            a.AccessableUsers.Add(account);
            SaveObject(a);
        }

        public void GiveAccessToGroup(long attachmentId, long groupId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            Group group = StructureMap.ObjectFactory.GetInstance<IGroupRepository>().GetObjectById(groupId);
            a.AccessableGroups.Add(group);
            SaveObject(a);
        }

        public void GiveAccessToClass(long attachmentId, long classId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            ClassInfo clazz = StructureMap.ObjectFactory.GetInstance<IClassInfoRepository>().GetObjectById(classId);
            a.AccessableClasses.Add(clazz);
            SaveObject(a);
        }
        
        public void RemoveAccessForUser(long attachmentId,long userId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            Account account = StructureMap.ObjectFactory.GetInstance<IAccountRepository>().GetObjectById(userId);
            a.AccessableUsers.Remove(account);
            SaveObject(a);
        }

        public void RemoveAccessForGroup(long attachmentId, long groupId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            Group group = StructureMap.ObjectFactory.GetInstance<IGroupRepository>().GetObjectById(groupId);
            a.AccessableGroups.Remove(group);
            SaveObject(a);
        }

        public void RemoveAccessForClass(long attachmentId, long classId)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            ClassInfo clazz = StructureMap.ObjectFactory.GetInstance<IClassInfoRepository>().GetObjectById(classId);
            a.AccessableClasses.Remove(clazz);
            SaveObject(a);
        }

        public void SetPublic(long attachmentId, bool isPublic)
        {
            Attachment a = GetObject(attachmentId);
            if (((Account)Server.HttpContext.Session["Account"]).Id != a.UploaderId)
                throw new Exception("只有附件上传者才可以修改附件的访问者");

            a.IsPublicAttachment = isPublic;
            SaveObject(a);
        }
    }
}
