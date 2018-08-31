using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ILeaf.Web.Hubs
{
    public class ILeafHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void ReportOnline(string connId,long userId)
        {

        }
    }
}