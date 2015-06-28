using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
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
        const string CurrentUsername = "Sergey";

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
            SystemActors.SignalRActor.Tell(new StartNewSessionCommand(new UserSession(CurrentUsername, SystemActors.SignalRActor)));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // fake auth
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, CurrentUsername)
            };
            var currentPrinciple = new ClaimsPrincipal(new ClaimsIdentity(claims));
            HttpContext.Current.User = currentPrinciple;
            Thread.CurrentPrincipal = currentPrinciple;
        }
    }
}
