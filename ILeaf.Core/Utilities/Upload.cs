using ILeaf.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ILeaf.Core.Utilities
{
    public class Upload
    {
        #region 通用方法

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="saveOnServerPath">保存到服务器路径（"~/Upload/"下面）</param>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="fileNameOnServer">保存文件名（不包含扩展名）</param>
        /// <param name="limit">限制大小（KB）</param>
        /// <param name="isDel">是否删除已存在</param>
        /// <returns></returns>
        public static string UploadFile_Img(string saveOnServerPath, HttpPostedFileBase file, string fileNameOnServer, long limit, bool isDel, string[] allowedExtension)
        {
            allowedExtension = allowedExtension ?? new string[] { ".gif", ".png", ".bmp", ".jpg" };//允许扩展名
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            
            if (allowedExtension.Contains(fileExtension))
            {
                
                if (file.ContentLength < limit * 1024)
                {
                    if (!saveOnServerPath.EndsWith("/"))
                    {
                        saveOnServerPath += "/";
                    }
                    return UploadFile("~/Upload/" + saveOnServerPath, fileNameOnServer, file, limit, isDel);
                }
                else
                {
                    return "只能上传" + limit + "KB以内的文件！此文件大小：" + file.ContentLength / 1024 + " KB";
                }
            }
            else
            {
                return "只能上传图片文件！";
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="savePathStr"></param>
        /// <param name="file">HttpPostedFileBase</param>
        /// <param name="fileNameOnServer">保存文件名（包含扩展名）</param>
        /// <param name="limit">限制大小（KB）</param>
        /// <param name="isDel">是否删除已存在</param>
        /// <returns></returns>
        public static string UploadFile(string savePathStr, string fileNameOnServer, HttpPostedFileBase file, long limit, bool isDel, string[] allowedExtension = null, string[] deniedExtension = null)
        {

            string saveFileName = string.Empty;


            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            if (allowedExtension != null && !allowedExtension.Contains(fileExtension))
            {
                return "为确保系统安全，此文件类型（{0}）被禁止上传，如确实需要上传，请联系客服。".With(fileExtension);
            }

            if(deniedExtension !=null && deniedExtension.Contains(fileExtension))
            {
                return "为确保系统安全，此文件类型（{0}）被禁止上传，如确实需要上传，请联系客服。".With(fileExtension);
            }
            
            if (file.ContentLength < limit * 1024)
            {
                saveFileName = fileNameOnServer;
                string savePhyicalPath = System.Web.HttpContext.Current.Server.MapPath(savePathStr);
                string savePhyicalFilePath = Path.Combine(savePhyicalPath, saveFileName);
                string saveApplicationPath = Path.Combine(GetFullApplicationPathFromVirtualPath(savePathStr), saveFileName);

               
                if (isDel && File.Exists(savePhyicalFilePath))
                {
                    File.Delete(savePhyicalFilePath);
                }

                if (!Directory.Exists(savePhyicalPath))
                {
                    Directory.CreateDirectory(savePhyicalPath);
                }
                file.SaveAs(savePhyicalFilePath);

                
                return saveApplicationPath;
            }
            else
            {
                return "只能上传" + limit + "KB以内的文件！此文件大小：" + file.ContentLength / 1024 + " KB";
            }
            
        }

        /// <summary>
        /// 从应用程序虚拟路径，获取应用程序路径（相对网站根目录，/开头）
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFullApplicationPathFromVirtualPath(string virtualPath)
        {
            return virtualPath.Replace("~/", HttpContext.Current.Request.ApplicationPath);
        }

        #endregion


        public static string UpLoadAvator(HttpPostedFileBase file)
        {
            string newFilename = DateTime.Now.Ticks.ToString();
            string[] allowedExtension = new string[] { ".jpg", ".bmp", ".png", ".jpeg", ".gif" };
            string uploadResult = UploadFile_Img("Avators", file, newFilename, 2 * 1024, true, allowedExtension);
            return uploadResult;
        }

        public static string UploadChatImage(HttpPostedFileBase file)
        {
            string newFilename = DateTime.Now.Ticks.ToString();
            string[] allowedExtension = new string[] { ".jpg", ".bmp", ".png", ".jpeg", ".gif" };
            string uploadResult = UploadFile_Img("ChatImg", file, newFilename, 2 * 1024, true, allowedExtension);
            return uploadResult;
        }

        public static string AdminUploadFile(HttpPostedFileBase file,string path)
        {
            string uploadResult = UploadFile(path, file.FileName, file, 2 * 1024 * 1024, true);
            return uploadResult;
        }

        /// <summary>
        /// 察看上传是否成功
        /// </summary>
        /// <param name="saveFileName">上传返回的字符串，如果“/”开头，则表示上传成功，不然里面包含的是错误信息</param>
        /// <returns></returns>
        public static bool CheckUploadSuccessful(string saveFileName)
        {
            if (saveFileName != null && saveFileName.StartsWith("/"))
                return true;
            else
                return false;
        }
    }
}
