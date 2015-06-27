using Akka.Actor;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;
using Twitter.Service.Actors;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;

namespace Twitter.Service
{
    public class ServiceHost : ServiceControl
    {
        protected ActorSystem ActorSystem { get; private set; }

        public bool Start(HostControl hostControl)
        {
            ActorSystem = ActorSystem.Create("twitterservice");
            SystemActors.ApiActor = ActorSystem.ActorOf(Props.Create(() => new ApiActor()), "api");
            EmulateConnectionsAsync();
            return true;
        }

        private void EmulateConnectionsAsync()
        {
            Task.Delay(10000)
                .ContinueWith(_ =>
                {
                    foreach (var username in new[] {"John", "Ivan", "Ann", "Alex"})
                    {
                        Thread.Sleep(250);
                        SystemActors.ApiActor.Tell(
                            new StartNewSessionCommand(new UserSession(username, ActorRefs.Nobody)));
                    }
                });
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }
    }
}
