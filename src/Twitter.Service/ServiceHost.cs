using Akka.Actor;
using Topshelf;

namespace Twitter.Service
{
    public class ServiceHost : ServiceControl
    {
        public ActorSystem ActorSystem { get; private set; }

        public bool Start(HostControl hostControl)
        {
            ActorSystem = ActorSystem.Create("twitterservice");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }
    }
}
