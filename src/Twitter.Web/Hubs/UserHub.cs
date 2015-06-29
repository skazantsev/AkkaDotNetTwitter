using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Twitter.Web.Hubs
{
    [HubName("userHub")]
    public class UserHub : Hub
    {
        public void NewUserConnected(string username)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.All.userConnected(username);
        }

        public void NewMessagePosted(string username, string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
            context.Clients.All.messagePosted(username, message);
        }
    }
}