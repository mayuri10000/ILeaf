using ILeaf.Core.Models;
using ILeaf.Service;
using ILeaf.Web.Controllers;
using ILeaf.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ILeaf.Web.Areas.Web.Controllers
{
    [ILeafAuthorize]
    public class AttachmentsController : BaseController
    {
        IAttachmentService service = StructureMap.ObjectFactory.GetInstance<IAttachmentService>();

        public ActionResult Download(string attachmentId)
        {
            try
            {
                if (!service.HasAccess(Int64.Parse(attachmentId)))
                {
                    return Content("您没有权限下载该附件");
                }
                else
                {
                    Attachment attachment = service.GetObject(Int64.Parse(attachmentId));
                    return File(new FileStream(attachment.StoragePath, FileMode.Open), "application/octet-stream");
                }
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult UploadForCourse()
        {
            try
            {
                string courseId = Request.Form["courseId"];
                string courseTime = Request.Form["courseTime"];

                if (Request.Files.Count == 0)
                    return Content("您没有上传文件");

                var file = Request.Files[0];
                var attachment = service.SaveAttachment(file);
                ICourseService courseService = StructureMap.ObjectFactory.GetInstance<ICourseService>();
                //var classes = courseService.GetObject(Int64.Parse(courseId)).Classes;
                //foreach(var clas in classes)
                //{
                //    service.GiveAccessToClass(attachment.Id, clas.Id);
                //}
                attachment.AttachmentCourses.Add(new AttachmentCourse()
                {
                    AttachmentId = attachment.Id,
                    CourseId = Int64.Parse(courseId),
                    CourseTime = DateTime.Parse(courseTime)
                });

                service.SaveObject(attachment);

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult UploadForGroup()
        {
            try
            {
                string groupId = Request.QueryString["groupId"];

                if (Request.Files.Count == 0)
                    return Content("您没有上传文件");

                var file = Request.Files[0];
                var attachment = service.SaveAttachment(file);
                service.GiveAccessToGroup(attachment.Id, Int64.Parse(groupId));

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpPost]
        public ActionResult UploadForUser()
        {
            try
            {
                string userId = Request.QueryString["userId"];

                if (Request.Files.Count == 0)
                    return Content("您没有上传文件");

                var file = Request.Files[0];
                var attachment = service.SaveAttachment(file);
                service.GiveAccessToUser(attachment.Id, Int64.Parse(userId));

                return Content("success");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}