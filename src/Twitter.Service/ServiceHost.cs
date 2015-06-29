using System;
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
            var users = new[] {"John", "Ivan", "Ann", "Alex"};
            var rand = new Random();
            Task.Delay(10000)
                .ContinueWith(_ =>
                {
                    foreach (var username in users)
                    {
                        Thread.Sleep(250);
                        SystemActors.ApiActor.Tell(
                            new StartNewSessionCommand(new UserSession(username, ActorRefs.Nobody)));
                    }
                })
                .ContinueWith(_ =>
                {
                    for (var i = 0; i < 1000; ++i)
                    {
                        Thread.Sleep(rand.Next(500, 1000));
                        var userIndex = rand.Next(0, users.Length);
                        SystemActors.ApiActor.Tell(new NewMessagePosted(users[userIndex], "Test"));
                    }
                    SystemActors.ApiActor.Tell(new NewMessagePosted("Administrator", "The END"));
                });
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }
    }
}
