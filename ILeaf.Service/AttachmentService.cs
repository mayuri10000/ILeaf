using ILeaf.Core;
using ILeaf.Core.Models;
using ILeaf.Core.Utilities;
using ILeaf.Repository;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace ILeaf.Service
{
    public interface IAttachmentService : IBaseService<Attachment>
    {
        void SaveAttachment(HttpPostedFileBase file, long userId);
        bool HasAccess(long attachmentId, long userId);
        void DeleteAttachment(long attachmentId);
        void DeleteAllExpiredAttachment();
        void UpdateAttachment(long attachmentId, HttpPostedFileBase file);
        void GiveAccessToUser(long attachmentId, long userId);
        void GiveAccessToGroup(long attachmentId, long groupId);
        void SetPublic(long attachmentId, bool isPublic);
    }

    public class AttachmentService : BaseService<Attachment>, IAttachmentService
    {
        private IAttachmentAccessRepository access_repo = StructureMap.ObjectFactory.GetInstance<IAttachmentAccessRepository>();

        public AttachmentService(IBaseRepository<Attachment> repo) : base(repo)
        {
        }

        public void SaveAttachment(HttpPostedFileBase file, long userId)
        {
            string filename =  Guid.NewGuid().ToString();

            Upload.UploadFile("~/Upload/Attachments/", filename, file, 1024 * 100, false);

            Attachment a = new Attachment()
            {
                UploaderId = userId,
                FileName = file.FileName,
                Size = file.ContentLength,
                StoragePath = Server.GetMapPath("~/Upload/Attachments/" + filename),
                UploadTime = DateTime.Now,
                ExpireTime = DateTime.Now.AddDays(30),
                IsPublic = false,
            };

            SaveObject(a);
        }

        public bool HasAccess(long attachmentId, long userId)
        {
            Attachment attachment = GetObject(attachmentId);
            if (attachment == null)
                throw new Exception("附件不存在或已过期");

            if (attachment.UploaderId == userId)
                return true;

            var accesses = attachment.AttachmentAccesses;
            IGroupMemberRepository gm = StructureMap.ObjectFactory.GetInstance<IGroupMemberRepository>();
            foreach(AttachmentAccess access in accesses)
            {
                if (!access.IsGroup && access.AccessorId == userId)
                    return true;
                if (access.IsGroup)
                {
                    var gms = gm.GetFirstOrDefaultObject(x => x.GroupId == access.AccessorId && !x.IsMemberGroup && x.IsAccepted && x.MemberId == userId);
                    if (gms != null)
                        return true;
                }
            }

            return false;
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
            a.Size = file.ContentLength;

            SaveObject(a);
        }

        private static string GetMD5HashFromFile(string fileName)
        {
            
                FileStream file = new FileStream(fileName, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            
        }

        public void GiveAccessToUser(long attachmentId, long userId)
        {
            AttachmentAccess access = new AttachmentAccess()
            {
                AttachmentId = attachmentId,
                IsGroup = false,
                AccessorId = userId
            };
            access_repo.Save(access);
        }

        public void GiveAccessToGroup(long attachmentId, long groupId)
        {
            AttachmentAccess access = new AttachmentAccess()
            {
                AttachmentId = attachmentId,
                IsGroup = true,
                AccessorId = groupId
            };
            access_repo.Save(access);
        }

        public void SetPublic(long attachmentId, bool isPublic)
        {
            Attachment a = GetObject(attachmentId);
            a.IsPublic = isPublic;
            SaveObject(a);
        }
    }
}
