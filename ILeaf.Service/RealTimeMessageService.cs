using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILeaf.Service
{
    public static class RealTimeMessageService
    {
        public static Func<long, string, MessageLevel, long?, long?, bool> OnMessage;

        public static void SendMessage(long senderId, string content, MessageLevel messageType, long? userId, long? groupId)
        {
            OnMessage(senderId, content, messageType, userId, groupId);
        }
    }
}
