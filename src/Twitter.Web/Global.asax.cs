using Akka.Actor;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Twitter.Shared.Actors;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;
using Twitter.Web.Actors;

namespace Twitter.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var actorSystem = ActorSystem.Create("twitterweb");

            SystemActors.RemoteApiActor =
                actorSystem.ActorOf(Props.Create(() => new RemoteApiActor()), "remoteApi");

            SystemActors.SignalRActor =
                actorSystem.ActorOf(Props.Create(() => new SignalRActor(SystemActors.RemoteApiActor)), "signalr");

            InitUser();
        }

        private void InitUser()
        {
            const string username = "Sergey";
            SystemActors.SignalRActor.Tell(new StartNewSessionCommand(new UserSession(username, SystemActors.SignalRActor)));
        }
    }
}
