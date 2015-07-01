using Akka.Actor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Twitter.Service.Actors;
using Twitter.Shared.Business;
using Twitter.Shared.Messages;

namespace Twitter.Service.Components
{
    public class TwitterFeedEmulator
    {
        private List<UserData> _users;
 
        public void EmulateUserActivity()
        {
            ReadUsers();
            Task.Delay(8000)
                .ContinueWith(_ => ConnectUsers())
                .ContinueWith(_ => StartTweeting());
        }

        private void ReadUsers()
        {
            _users = JsonConvert.DeserializeObject<List<UserData>>(File.ReadAllText("userdata.json"));
        }

        private void ConnectUsers()
        {
            foreach (var user in _users)
            {
                Thread.Sleep(250);
                SystemActors.ApiActor.Tell(
                    new StartNewSessionCommand(new UserSession(user.Username, ActorRefs.Nobody)));
            }
        }

        private void StartTweeting()
        {
            var rand = new Random();
            for (var i = 0; i < 1000; ++i)
            {
                Thread.Sleep(rand.Next(1000, 2000));
                var user = _users[rand.Next(0, _users.Count)];
                SystemActors.ApiActor.Tell(new NewTweetPosted(user.Username, user.Tweets[i % user.Tweets.Count]));
            }
            SystemActors.ApiActor.Tell(new NewTweetPosted("UFO", "The emulator has finished its work"));
        }

        protected class UserData
        {
            public string Username { get; set; }

            public List<string> Tweets { get; set; }
        }
    }
}
