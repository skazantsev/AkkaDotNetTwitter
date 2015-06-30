using Akka.Actor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            var users = JsonConvert.DeserializeObject<List<UserData>>(File.ReadAllText("userdata.json"));

            var rand = new Random();
            Task.Delay(10000)
                .ContinueWith(_ =>
                {
                    foreach (var user in users)
                    {
                        Thread.Sleep(250);
                        SystemActors.ApiActor.Tell(
                            new StartNewSessionCommand(new UserSession(user.Username, ActorRefs.Nobody)));
                    }
                })
                .ContinueWith(_ =>
                {
                    for (var i = 0; i < 1000; ++i)
                    {
                        Thread.Sleep(rand.Next(200, 1000));
                        var userIndex = rand.Next(0, users.Count);
                        SystemActors.ApiActor.Tell(new NewMessagePosted(users[userIndex].Username, users[userIndex].Messages[i % users[userIndex].Messages.Count]));
                    }
                    SystemActors.ApiActor.Tell(new NewMessagePosted("Administrator", "The END"));
                });
        }

        public bool Stop(HostControl hostControl)
        {
            ActorSystem.Shutdown();
            return true;
        }

        public class UserData
        {
            [JsonProperty(PropertyName = "username")]
            public string Username { get; set; }

            [JsonProperty(PropertyName = "messages")]
            public List<string> Messages { get; set; }
        }
    }
}
