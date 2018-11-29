using System;
using System.Web;
using Microsoft.AspNet.SignalR;
namespace Myndie
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string img)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, img);
        }
    }
}