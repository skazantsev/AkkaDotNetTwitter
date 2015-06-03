using Akka.Actor;
using Topshelf;

namespace TwitterFeed.Service
{
    public class FeedService : ServiceControl
    {
        public ActorSystem ActorSystem { get; private set; }

        public bool Start(HostControl hostControl)
        {
            ActorSystem = ActorSystem.Create("feedservice");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }
    }
}
