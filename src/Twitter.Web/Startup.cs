using Microsoft.Owin;
using Owin;
using Twitter.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Twitter.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}