using ILeaf.Core.Models;
using ILeaf.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ILeaf.Service
{
    public class ChatMessageService : BaseService<ChatMessage>
    {
        public ChatMessageService(ChatMessageRepository repo) : base(repo) { }

        public void SendMessage(long userId,MessageType type,string content)
        {

        }
    }
}
