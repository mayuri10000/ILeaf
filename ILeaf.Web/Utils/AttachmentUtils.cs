using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ILeaf.Web.Utils
{
    public static class AttachmentUtils
    {
        public static string FormatFileSize(long fileSize)
        {
            if (fileSize < 1024)
                return String.Format("{0} B", fileSize);
            if (fileSize > 1024 && fileSize < 1024 * 1024)
                return String.Format("{0:F} KB", fileSize / 1024f);
            if (fileSize > 1024 * 1024 && fileSize < 1024 * 1024 * 1024)
                return String.Format("{0:F} MB", fileSize / (1024f * 1024));
            if (fileSize > 1024 * 1024 * 1024)
                return string.Format("{0:F} GB", fileSize / (1024f * 1024 * 1024));
            else return "";
        }

        public static string GetIconForFile(string fileName)
        {
            string extension = fileName.Substring(fileName.LastIndexOf('.'));
            switch (extension)
            {
                case ".zip":
                case ".rar":
                case ".7z":
                    return "fa fa-file-archive-o";
                case ".mp3":
                case ".wav":
                case ".flac":
                case ".wma":
                    return "fa fa-file-audio-o";
                case ".htm":
                case ".html":
                case ".js":
                case ".css":
                case ".c":
                case ".cc":
                case ".cpp":
                case ".cs":
                case ".java":
                case ".py":
                case ".xml":
                    return "fa fa-file-code-o";
                case ".xls":
                case ".xlsx":
                case ".xlt":
                case ".xltx":
                    return "fa fa-file-excel-o";
                case ".jpg":
                case ".jpeg":
                case ".bmp":
                case ".png":
                case ".gif":
                    return "fa fa-file-image-o";
                case ".mp4":
                case ".flv":
                case ".f4v":
                case ".ts":
                case ".mov":
                case ".mkv":
                case ".rm":
                case ".rmvb":
                case ".3gp":
                    return "fa fa-file-movie-o";
                case ".pdf":
                    return "fa fa-file-pdf-o";
                case ".ppt":
                case ".pptx":
                case ".pps":
                case ".ppsx":
                case ".pot":
                case ".potx":
                    return "fa fa-file-powerpoint-o";
                case ".doc":
                case ".docx":
                case ".dot":
                case ".dotx":
                case ".rtf":
                    return "fa fa-file-word-o";
                case ".txt":
                case ".log":
                    return "fa fa-file-text-o";
                    return "fa fa-file-text-o";
                default:
                    return "fa fa-file-o";
            }
        }
    }
}