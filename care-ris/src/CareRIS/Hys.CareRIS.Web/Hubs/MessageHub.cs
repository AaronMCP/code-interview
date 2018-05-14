using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Hys.CareRIS.Web.Hubs
{
    public class MessageHub : Hub
    {

        public void Send(string message, string messageparams)
        {

            Clients.All.broadcastMessage(message, messageparams);
        }
    }
}