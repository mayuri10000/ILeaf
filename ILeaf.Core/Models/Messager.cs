/*  Messager.cs  全局提示消息
 *  本文件属于北京邮电大学创新项目作品ILeaf
 *  作者：刘同 2017212783
 *  最后修改日期：2018年7月9日
 */

using System;

namespace ILeaf.Core.Models
{
    public enum MessageLevel
    {
        Success,
        Error,
        Information,
        Attention
    }

    [Serializable]
    public class Messager
    {
        public MessageLevel MessageType { get; set; }
        public string MessageText { get; set; }
        public bool ShowClose { get; set; }
        public Messager(MessageLevel messageType, string messageText, bool showClose = true)
        {
            MessageType = messageType; MessageText = messageText;
            ShowClose = showClose;
        }
    }
}
