using Akka.Actor;
using Topshelf;
using Twitter.Service.Actors;
using Twitter.Service.Components;

namespace Twitter.Service
{
    public class ServiceHost : ServiceControl
    {
        protected ActorSystem ActorSystem { get; private set; }

        public bool Start(HostControl hostControl)
        {
            ActorSystem = ActorSystem.Create("twitterservice");
            SystemActors.ApiActor = ActorSystem.ActorOf(Props.Create(() => new ApiActor()), "api");
            new TwitterFeedEmulator().EmulateUserActivity();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }
    }
}
